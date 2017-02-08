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
            oldName = sysShortcut.Name;
            IsExisting = true;
        }

        #region UIProperties
        public string Name
        {
            get { return sysShortcut.Description; }
            set
            {
                sysShortcut.Description = value;
                ChangesPending = true;
                RaisePropertyChanged();
            }
        }



        public string Shortcut
        {
            get { return sysShortcut.Name; }
            set
            {
                sysShortcut.Name = value;
                ChangesPending = true;
                RaisePropertyChanged();
            }
        }

        public string StartupPath
        {
            get { return sysShortcut.StartupPath; }
            set
            {
                sysShortcut.StartupPath = value;
                ChangesPending = true;
                RaisePropertyChanged();
            }
        }

        public string Target
        {
            get { return sysShortcut.Target; }
            set
            {
                sysShortcut.Target = value;
                ChangesPending = true;
                RaisePropertyChanged();
            }
        }


        public string Arguments
        {
            get { return sysShortcut.Arguments; }
            set
            {
                sysShortcut.Arguments = value;
                ChangesPending = true;
                RaisePropertyChanged();
            }
        }


        public WindowTypes WindowType
        {
            get { return sysShortcut.WindowType; }
            set
            {
                sysShortcut.WindowType = value;
                ChangesPending = true;
                RaisePropertyChanged();
            }
        }



        private bool _changesPending;

        public bool ChangesPending
        {
            get { return _changesPending; }
            private set
            {
                _changesPending = value;
                RaisePropertyChanged();
            }
        }

        public bool IsExisting { get; private set; }

        private bool Check()
        {
            return sysShortcut.Check() && ChangesPending;
        }

        private void InitializeCommand()
        {
            SaveCommand = new RelayCommand(Save, Check);
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
            if (Check())
            {

                try
                {
                    sysShortcut.Save(oldName);
                    IsExisting = true;
                    oldName = sysShortcut.Name;
                    ChangesPending = false;
                }
                catch (InvalidOperationException ex)
                {

                    Messenger.Default.Send(new NotificationMessage(this, ex.Message), "UI_MSG");
                }
            }
        }
        public void Delete()
        {
            if (IsExisting)
            {
                sysShortcut.Delete();
            }
        }
    }
}