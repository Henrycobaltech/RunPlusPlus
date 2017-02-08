using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using RunPlusPlus.ViewModel;
using System.Windows;

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
            DataContext = new MainWindowViewModel();
            Messenger.Default.Register<NotificationMessage>(this, MessageTokens.UIMessage, 
                m => MessageBox.Show(m.Notification));
            Messenger.Default.Register<NotificationMessage>(this, MessageTokens.ShowAbout, 
                m => new AboutWindow { Owner = this }.ShowDialog());
        }
        private void OnBrowseTargetButtonClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if ((bool)dlg.ShowDialog())
            {
                targetTextBox.Text = dlg.FileName;
            }
        }

        private void OnBrowseStartupPathButtonClick(object sender, RoutedEventArgs e)
        {
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                startupPathTextBox.Text = dlg.SelectedPath;
            }
        }
    }



}
