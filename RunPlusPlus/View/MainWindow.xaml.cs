using GalaSoft.MvvmLight.Messaging;
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
    public partial class MainWindow : Window
    {
        private const int WM_DWMCOMPOSITIONCHANGED = 0x31a;
        private const int WM_THEMECHANGED = 0x31e;

        static readonly WindowChrome chrome = new WindowChrome() { GlassFrameThickness = new Thickness(15, 70, 15, 15), CaptionHeight = 50, };
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            Messenger.Default.Register<NotificationMessage>(this, "UI_MSG", m => MessageBox.Show(m.Notification));
            Messenger.Default.Register<NotificationMessage>(this, "SHOW_ABOUT", m => new AboutWindow() { Owner = this }.ShowDialog());
            if (DwmIsCompositionEnabled())
            {
                WindowChrome.SetWindowChrome(this, chrome);
            }
            else
            {
                this.Background = new SolidColorBrush(SystemColors.WindowColor);
            }
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            (PresentationSource.FromVisual(this) as HwndSource).AddHook(WndProc);
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            if (msg == WM_DWMCOMPOSITIONCHANGED || msg == WM_THEMECHANGED)
            {
                if (DwmIsCompositionEnabled())
                {
                    this.Background = Brushes.Transparent;
                    WindowChrome.SetWindowChrome(this, chrome);
                }
                else
                {
                    this.Background = new SolidColorBrush(SystemColors.WindowColor);
                    WindowChrome.SetWindowChrome(this, null);
                }
            }
            handled = false;
            return IntPtr.Zero;
        }


        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

    }



}
