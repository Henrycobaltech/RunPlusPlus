using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using RunPlusPlus.Services;
using System.Collections.ObjectModel;

namespace RunPlusPlus.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ShortcutViewModel _selectedItem;

        private ObservableCollection<ShortcutViewModel> _shortCuts = new ObservableCollection<ShortcutViewModel>();

        public MainWindowViewModel()
        {
            InitializeCommands();
            LoadShortcuts();
            ShortcutServices.Changed += (o, e) => LoadShortcuts();
        }

        private void LoadShortcuts()
        {
            Shortcuts.Clear();
            foreach (var item in ShortcutServices.LoadExistingShortcuts())
            {
                Shortcuts.Add(new ShortcutViewModel(item));
            }
        }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand RemoveCommand { get; set; }

        public ShortcutViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ShortcutViewModel> Shortcuts
        {
            get { return _shortCuts; }
            set
            {
                _shortCuts = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ShowAboutCommand { get; set; }

        private void AddItem()
        {
            var item = new ShortcutViewModel();
            Shortcuts.Add(item);
            SelectedItem = item;
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem, () => SelectedItem != null);
            ShowAboutCommand = new RelayCommand(
                () =>
                {
                    Messenger.Default.Send(new NotificationMessage(this, ""), MessageTokens.ShowAbout);
                });
        }
        private void RemoveItem()
        {
            var index = Shortcuts.IndexOf(SelectedItem);
            SelectedItem.Delete();
            Shortcuts.Remove(SelectedItem);
            if ((index + 1) > Shortcuts.Count)
            {
                index--;
            }
            if (Shortcuts.Count != 0)
            {
                SelectedItem = Shortcuts[index];
            }
        }
    }
}