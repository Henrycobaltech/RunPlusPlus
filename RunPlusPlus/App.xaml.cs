using RunPlusPlus.Services;
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
            ShortcutServices.InitializeEnvironmentVariable();
            DispatcherUnhandledException += (o, ev) => MessageBox.Show(ev.Exception.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
