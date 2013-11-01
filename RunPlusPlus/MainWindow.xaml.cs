using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using RunPlusPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RunPlusPlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            Messenger.Default.Register<NotificationMessage>(this, "UI_MSG", m => MessageBox.Show(m.Notification));
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
