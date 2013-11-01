using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RunPlusPlus.Model;
using RunPlusPlus.Services;
using System;

namespace RunPlusPlus.ViewModel
{
    internal class ShortcutViewModel : ViewModelBase
    {

        private Shortcut sysShortcut = new Shortcut();
        private string oldName = "";

        public ShortcutViewModel()
        {
            InitializeCommand();
        }

        public ShortcutViewModel(Shortcut sysShortcut)
            : this()
        {
            if (string.IsNullOrEmpty(sysShortcut.Name))
            {
                throw new ArgumentException("Name of the shortcut can not be null");
            }
            this.sysShortcut = sysShortcut;
            this.oldName = sysShortcut.Name;
        }

        public string Name
        {
            get { return this.sysShortcut.Description; }
            set
            {
                this.sysShortcut.Description = value;
                this.IsSaved = false;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }

        public string Shortcut
        {
            get { return this.sysShortcut.Name; }
            set
            {
                this.sysShortcut.Name = value;
                this.IsSaved = false;
                this.RaisePropertyChanged();
            }
        }

        public string StartupPath
        {
            get { return this.sysShortcut.StartupPath; }
            set
            {
                this.sysShortcut.StartupPath = value;
                this.IsSaved = false;
                this.RaisePropertyChanged();
            }
        }

        public string Target
        {
            get { return this.sysShortcut.Target; }
            set
            {
                this.sysShortcut.Target = value;
                this.IsSaved = false;
                this.RaisePropertyChanged();
            }
        }

        public WindowTypes WindowType
        {
            get { return this.sysShortcut.WindowType; }
            set
            {
                this.sysShortcut.WindowType = value;
                this.IsSaved = false;
                this.RaisePropertyChanged();
            }
        }

        public void Delete()
        {
            this.sysShortcut.Delete();
        }

        private bool Check()
        {
            return this.sysShortcut.Check() && (!this.IsSaved);
        }

        private void InitializeCommand()
        {
            this.SaveCommand = new RelayCommand(this.Save, this.Check);
        }

        // will be avaliable in later version

        //private bool _isRunAsAdministrstor;

        //public bool IsRunAsAdministrator
        //{
        //    get { return _isRunAsAdministrstor; }
        //    set
        //    {
        //        _isRunAsAdministrstor = value;
        //        this.RaisePropertyChanged();
        //    }
        //}

        private void Save()
        {
            if (this.Check())
            {
                this.sysShortcut.Save(this.oldName);
                this.oldName = this.sysShortcut.Name;
                this.IsSaved = true;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private bool _isSaved = true;

        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                this.RaisePropertyChanged();
            }
        }

    }
}