using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RunPlusPlus.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace RunPlusPlus.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ShortcutViewModel _selectedItem;

        private ObservableCollection<ShortcutViewModel> _shorts = new ObservableCollection<ShortcutViewModel>();

        public MainWindowViewModel()
        {
            this.InitializeCommands();
            LoadShortcuts();
            ShortcutServices.Saved += (o, e) => this.LoadShortcuts();
        }

        private void LoadShortcuts()
        {
            this.Shortcuts.Clear();
            foreach (var item in ShortcutServices.LoadExistingShortcuts())
            {
                this.Shortcuts.Add(new ShortcutViewModel(item));
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
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<ShortcutViewModel> Shortcuts
        {
            get { return _shorts; }
            set
            {
                _shorts = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand ShowAboutCommand { get; set; }

        private void AddItem()
        {
            var item = new ShortcutViewModel();
            this.Shortcuts.Add(item);
            this.SelectedItem = item;
        }

        private void InitializeCommands()
        {
            this.AddCommand = new RelayCommand(this.AddItem);
            this.RemoveCommand = new RelayCommand(this.RemoveItem,
                () =>
                {
                    return this.SelectedItem != null;
                });
        }
        private void RemoveItem()
        {
            var index = this.Shortcuts.IndexOf(this.SelectedItem);
            this.SelectedItem.Delete();
            this.Shortcuts.Remove(this.SelectedItem);
            if ((index + 1) > this.Shortcuts.Count)
            {
                index--;
            }
            if (this.Shortcuts.Count != 0)
            {
                this.SelectedItem = this.Shortcuts[index];
            }
        }
    }
}