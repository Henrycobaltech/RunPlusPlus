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
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (Environment.OSVersion.Version < new Version(6, 1))
            {
                MessageBox.Show("This application is only supported on Windows 7, Windows Server 2008 R2, or higher",
                    "Unsupported OS", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            await ShortcutServices.InitializeEnvironmentVariable();
            this.DispatcherUnhandledException += (o, ev) => MessageBox.Show(ev.Exception.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
