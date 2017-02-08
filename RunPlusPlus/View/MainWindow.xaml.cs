using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using RunPlusPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace RunPlusPlus.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            Messenger.Default.Register<NotificationMessage>(this, "UI_MSG", m => MessageBox.Show(m.Notification));
            Messenger.Default.Register<NotificationMessage>(this, "SHOW_ABOUT", m => new AboutWindow() { Owner = this }.ShowDialog());
        }
        private void OnBrowseTargetButtonClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if ((bool)dlg.ShowDialog())
            {
                this.targetTextBox.Text = dlg.FileName;
            }
        }

        private void OnBrowseStartupPathButtonClick(object sender, RoutedEventArgs e)
        {
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.startupPathTextBox.Text = dlg.SelectedPath;
            }
        }
    }



}
