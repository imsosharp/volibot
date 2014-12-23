#region

using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace Ini
{
    /// <summary>
    ///     Create a New INI file to store or load data
    /// </summary>
    public class IniFile
    {
        public string Path;

        /// <summary>
        ///     INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string iniPath)
        {
            Path = iniPath;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        ///     Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        /// <summary>
        ///     Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string section, string key)
        {
            var temp = new StringBuilder(255);
            var i = GetPrivateProfileString(section, key, "", temp,
                255, Path);
            return temp.ToString();
        }
    }
}