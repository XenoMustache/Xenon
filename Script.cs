using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Xenon.Engine {
	public class Script {
		readonly string name;
		bool canExecute;

		Assembly asm;

		public Script(string name) {
			this.name = name;
		}

		public void Compile(string file) {
			if (File.Exists(file)) {
				Console.Write($"Compiling \"{file}\"...");

				var script = File.ReadAllText(file);
				var syntaxTree = CSharpSyntaxTree.ParseText(script);

				string asmName = Path.GetRandomFileName();

				var refPaths = new[] {
					typeof(object).GetTypeInfo().Assembly.Location,
					typeof(Console).GetTypeInfo().Assembly.Location,
					Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
					GetType().GetTypeInfo().Assembly.Location
				};

				MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

				var compilation = CSharpCompilation.Create(
					asmName,
					syntaxTrees: new[] { syntaxTree },
					references: references,
					options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

				using (var ms = new MemoryStream()) {
					EmitResult result = compilation.Emit(ms);
					ms.Seek(0, SeekOrigin.Begin);

					try {
						asm = AssemblyLoadContext.Default.LoadFromStream(ms);
					}
					catch (BadImageFormatException) {
						Console.WriteLine("Bad format!");
						return;
					}
				}

				canExecute = true;
				Console.WriteLine("Success!");
			}
			else {
				canExecute = false;
				Console.WriteLine($"Script could not be found: \"{file}\"");
			}
		}

		public void ThreadedCompile(string file) {
			Thread childThread = new Thread(() => Compile(file));

			childThread.Start();
		}

		public void Execute(string method, object[] args = null) {
			if (canExecute) {
				var str = name.Remove(name.Length - 3);

				var type = asm.GetType($".{str}");
				var instance = asm.CreateInstance($".{str}");
				var meth = type.GetMember(method).First() as MethodInfo;

				var flags = BindingFlags.Public | BindingFlags.Static;

				meth.Invoke(instance, flags, null, args, null);
			}
		}
	}
}
