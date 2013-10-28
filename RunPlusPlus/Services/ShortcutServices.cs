using RunPlusPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace RunPlusPlus.Services
{
    static class ShortcutServices
    {
        private static readonly List<char> InvalidChars = new List<char>() { '<', '>', ',', '/', ',', '\\', ',', '|', ',', ':', ',', '"', '"', ',', '*', ',', '?' };

        private static void DirCheck()
        {
            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + @"\RunPlusPlus\Data\"))
            {
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + @"\RunPlusPlus\Data\");
            }
        }

        private static void SaveShortcut(Shortcut shortcut)
        {
            DirCheck();
            var path = GetShortcutPath(shortcut);
            var shell = new WshShell();
            var sc = (IWshShortcut)shell.CreateShortcut(path);
            sc.Description = shortcut.Description;
            sc.TargetPath = shortcut.Target;
            sc.WindowStyle = (int)shortcut.WindowType;
            sc.WorkingDirectory = shortcut.StartupPath;
            sc.Save();
        }

        private static string GetShortcutPath(Shortcut shortcut)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + @"\RunPlusPlus\Data\"
                + shortcut.Name + @".lnk";
        }

        internal static void Save(this Shortcut shortcut)
        {
            if (System.IO.File.Exists(GetShortcutPath(shortcut)))
            {
                throw new InvalidOperationException("The same shortcut is already exists.");
            }
            SaveShortcut(shortcut);
        }

        internal static void Delete(this Shortcut shortcut)
        {
            var path = GetShortcutPath(shortcut);
            System.IO.File.Delete(path);
        }

        internal static bool Check(this Shortcut shortcut)
        {
            if (string.IsNullOrEmpty(shortcut.Name) ||
                string.IsNullOrEmpty(shortcut.Target))
            {
                return false;
            }

            foreach (var item in InvalidChars)
            {
                if (shortcut.Name.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
