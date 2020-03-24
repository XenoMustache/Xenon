using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Xenon.Client.Graphics {
	public class Shader {
		public readonly int handle;
		private readonly Dictionary<string, int> _uniformLocations;

		public Shader(string vertexPath, string fragmentPath) {
			var shaderSource = LoadSource(vertexPath);

			var vertexShader = GL.CreateShader(ShaderType.VertexShader);

			GL.ShaderSource(vertexShader, shaderSource);

			CompileShader(vertexShader);

			shaderSource = LoadSource(fragmentPath);
			var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShader, shaderSource);
			CompileShader(fragmentShader);

			handle = GL.CreateProgram();

			GL.AttachShader(handle, vertexShader);
			GL.AttachShader(handle, fragmentShader);

			LinkProgram(handle);

			GL.DetachShader(handle, vertexShader);
			GL.DetachShader(handle, fragmentShader);
			GL.DeleteShader(fragmentShader);
			GL.DeleteShader(vertexShader);


			GL.GetProgram(handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

			_uniformLocations = new Dictionary<string, int>();

			for (var i = 0; i < numberOfUniforms; i++) {
				var key = GL.GetActiveUniform(handle, i, out _, out _);

				var location = GL.GetUniformLocation(handle, key);

				_uniformLocations.Add(key, location);
			}
		}

		private static void CompileShader(int shader) {
			GL.CompileShader(shader);

			GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
			if (code != (int)All.True) {
				var infoLog = GL.GetShaderInfoLog(shader);
				throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
			}
		}

		private static void LinkProgram(int program) {
			GL.LinkProgram(program);

			GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
			if (code != (int)All.True) {
				throw new Exception($"Error occurred whilst linking Program({program})");
			}
		}

		public void Use() {
			GL.UseProgram(handle);
		}

		public int GetAttribLocation(string attribName) {
			return GL.GetAttribLocation(handle, attribName);
		}

		private static string LoadSource(string path) {
			using (var sr = new StreamReader(path, Encoding.UTF8)) {
				return sr.ReadToEnd();
			}
		}

		public void SetInt(string name, int data) {
			GL.UseProgram(handle);
			GL.Uniform1(_uniformLocations[name], data);
		}

		public void SetFloat(string name, float data) {
			GL.UseProgram(handle);
			GL.Uniform1(_uniformLocations[name], data);
		}

		public void SetMatrix4(string name, Matrix4 data) {
			GL.UseProgram(handle);
			GL.UniformMatrix4(_uniformLocations[name], true, ref data);
		}

		public void SetVector3(string name, Vector3 data) {
			GL.UseProgram(handle);
			GL.Uniform3(_uniformLocations[name], data);
		}
	}
}
