using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RunPlusPlus.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunPlusPlus.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            this.InitializeCommands();
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

        private ObservableCollection<ShortcutViewModel> _shorts = new ObservableCollection<ShortcutViewModel>();

        public ObservableCollection<ShortcutViewModel> Shortcuts
        {
            get { return _shorts; }
            set
            {
                _shorts = value;
                this.RaisePropertyChanged();
            }
        }

        private ShortcutViewModel _selectedItem;

        public ShortcutViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                this.RaisePropertyChanged();
            }
        }


        public RelayCommand AddCommand { get; set; }

        public RelayCommand RemoveCommand { get; set; }

        public RelayCommand ShowAboutCommand { get; set; }


        private void AddItem()
        {
            var item = new ShortcutViewModel();
            this.Shortcuts.Add(item);
            this.SelectedItem = item;
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
