﻿/* This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://www.wtfpl.net/ for more details. */

using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace CFW2OFW
{
    class IniFile
    {
        private readonly string path;
        private readonly string Section = "CFW2OFW";

        static internal class NativeMethods
        {
            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            internal static extern int WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            internal static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        }

        public IniFile()
        {
            path = new FileInfo($@"{G.origDir}\CFW2OFW_settings.ini").FullName.ToString();
        }

        public string Read(string Key)
        {
            var RetVal = new StringBuilder(255);
            NativeMethods.GetPrivateProfileString(Section, Key, "", RetVal, 255, path);
            return RetVal.ToString().ToLower();
        }

        public void Write(string Key, string Value)
        {
            NativeMethods.WritePrivateProfileString(Section, Key, Value, path);
        }

        public bool KeyExists(string Key)
        {
            return Read(Key).Length > 0;
        }
    }
}