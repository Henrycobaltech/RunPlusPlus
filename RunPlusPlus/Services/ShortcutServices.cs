using IWshRuntimeLibrary;
using RunPlusPlus.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RunPlusPlus.Services
{
    internal static class ShortcutServices
    {
        private static readonly List<char> InvalidChars = new List<char>() { '<', '>', ',', '/', ',', '\\', ',', '|', ',', ':', ',', '"', '"', ',', '*', ',', '?' };

        private static readonly string dataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RunPlusPlus\Data\";

        internal static event EventHandler Saved;

        public static IEnumerable<Shortcut> LoadExistingShortcuts()
        {
            DirCheck();
            var dir = new DirectoryInfo(dataFolderPath);
            var lnks = dir.GetFiles(@"*.lnk");
            var shell = new WshShell();
            foreach (var item in lnks)
            {
                var sc = (IWshShortcut)shell.CreateShortcut(item.FullName);
                var shortcut = new Shortcut()
                {
                    Name = item.Name.Replace(@".lnk", ""),
                    Description = sc.Description,
                    Target = sc.TargetPath,
                    WindowType = (WindowTypes)sc.WindowStyle,
                    StartupPath = sc.WorkingDirectory,
                };
                yield return shortcut;
            }
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

        internal static void Delete(this Shortcut shortcut)
        {
            var path = GetShortcutPath(shortcut);
            System.IO.File.Delete(path);
        }

        internal static void Save(this Shortcut shortcut, string existingShortctName = "")
        {
            if (string.IsNullOrEmpty(existingShortctName))
            {
                ThrowIfExists(shortcut.Name);
            }
            if (!string.IsNullOrEmpty(existingShortctName)
                && existingShortctName != shortcut.Name)
            {
                System.IO.File.Move(GetShortcutPath(existingShortctName), GetShortcutPath(shortcut));
            }
            SaveShortcut(shortcut);
        }

        private static void ThrowIfExists(string name)
        {
            if (System.IO.File.Exists(GetShortcutPath(name)))
            {
                throw new InvalidOperationException("The same shortcut is already exists.");
            }
        }

        private static void DirCheck()
        {
            if (!System.IO.Directory.Exists(dataFolderPath))
            {
                System.IO.Directory.CreateDirectory(dataFolderPath);
            }
        }

        private static string GetShortcutPath(Shortcut shortcut)
        {
            return dataFolderPath + shortcut.Name + @".lnk";
        }

        private static string GetShortcutPath(string name)
        {
            return dataFolderPath + name + @".lnk";
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
            Saved(null, new EventArgs());
        }
    }
}