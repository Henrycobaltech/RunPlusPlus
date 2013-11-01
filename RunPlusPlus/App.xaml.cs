using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RunPlusPlus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
#if DEBUG
            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + @"\RunPlusPlus\Data\");
#endif
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RunPlusPlus\Data;";

            var sysPath = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.User);
            if (sysPath == null || !sysPath.Contains(path))
            {
                if (sysPath != null && sysPath.Last() != ';')
                {
                    sysPath += ';';
                }
                sysPath += path;
            }
            Environment.SetEnvironmentVariable("path", sysPath, EnvironmentVariableTarget.User);
        }
    }
}
