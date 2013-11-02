using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RunPlusPlus.View
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            this.versionText.Text = "Version: " + version.ToString();
        }

        static readonly Version version = Assembly.GetAssembly(typeof(AboutWindow)).GetName().Version;

        private void HyperLinkClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://rpp.codeplex.com/");
        }
    }
}
