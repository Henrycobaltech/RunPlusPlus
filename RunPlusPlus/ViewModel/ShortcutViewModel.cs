using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
            this.IsExisting = true;
        }

        #region UIProperties
        public string Name
        {
            get { return this.sysShortcut.Description; }
            set
            {
                this.sysShortcut.Description = value;
                this.ChangesPending = true;
                this.RaisePropertyChanged();
            }
        }



        public string Shortcut
        {
            get { return this.sysShortcut.Name; }
            set
            {
                this.sysShortcut.Name = value;
                this.ChangesPending = true;
                this.RaisePropertyChanged();
            }
        }

        public string StartupPath
        {
            get { return this.sysShortcut.StartupPath; }
            set
            {
                this.sysShortcut.StartupPath = value;
                this.ChangesPending = true;
                this.RaisePropertyChanged();
            }
        }

        public string Target
        {
            get { return this.sysShortcut.Target; }
            set
            {
                this.sysShortcut.Target = value;
                this.ChangesPending = true;
                this.RaisePropertyChanged();
            }
        }

        public WindowTypes WindowType
        {
            get { return this.sysShortcut.WindowType; }
            set
            {
                this.sysShortcut.WindowType = value;
                this.ChangesPending = true;
                this.RaisePropertyChanged();
            }
        }



        private bool _changesPending;

        public bool ChangesPending
        {
            get { return _changesPending; }
            private set
            {
                _changesPending = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsExisting { get; private set; }

        private bool Check()
        {
            return this.sysShortcut.Check() && this.ChangesPending;
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


        #endregion

        public RelayCommand SaveCommand { get; set; }
        private void Save()
        {
            if (this.Check())
            {

                try
                {
                    this.sysShortcut.Save(this.oldName);
                    this.IsExisting = true;
                    this.oldName = this.sysShortcut.Name;
                    this.ChangesPending = false;
                }
                catch (InvalidOperationException ex)
                {

                    Messenger.Default.Send(new NotificationMessage(this, ex.Message), "UI_MSG");
                }
            }
        }
        public void Delete()
        {
            if (this.IsExisting)
            {
                this.sysShortcut.Delete();
            }
        }
    }
}