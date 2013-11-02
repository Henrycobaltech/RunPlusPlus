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
            await ShortcutServices.InitializeEnvironmentVariable();
            this.DispatcherUnhandledException += (o, ev) => MessageBox.Show(ev.Exception.ToString());

        }
    }
}
