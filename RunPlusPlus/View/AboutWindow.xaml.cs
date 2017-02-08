using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;

namespace RunPlusPlus.View
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        static readonly Assembly assembly = Assembly.GetAssembly(typeof(AboutWindow));

        public AboutWindow()
        {
            InitializeComponent();
            var copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            versionText.Text = $"Version: {assembly.GetName().Version.ToString()}\n{copyright}";
        }


        private void HyperLinkClick(object sender, RoutedEventArgs e)
        {
            Process.Start((sender as Hyperlink).NavigateUri.ToString());
        }
    }
}
