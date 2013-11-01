using RunPlusPlus.Services;
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
            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RunPlusPlus\Data\");
#endif
            ShortcutServices.InitializeEnvironmentVariable();

            this.DispatcherUnhandledException += (o, ev) => MessageBox.Show(ev.Exception.ToString());

        }
    }
}
