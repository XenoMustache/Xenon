using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Xenon.Common.Utilities {
	public class Ini {
		readonly string exe = Directory.GetCurrentDirectory();
		string path;

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

		public Ini(string IniPath = null) { path = new FileInfo(IniPath ?? exe + ".ini").FullName.ToString(); }

		public string Read(string Key, string Section = null) {
			StringBuilder RetVal = new StringBuilder(255);
			GetPrivateProfileString(Section ?? exe, Key, "", RetVal, 255, path);
			return RetVal.ToString();
		}

		public void Write(string Key, string Value, string Section = null) { WritePrivateProfileString(Section ?? exe, Key, Value, path); }
		public void DeleteKey(string Key, string Section = null) { Write(Key, null, Section ?? exe); }
		public void DeleteSection(string Section = null) { Write(null, null, Section ?? exe); }
		public bool KeyExists(string Key, string Section = null) { return Read(Key, Section).Length > 0; }
	}
}
