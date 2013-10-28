using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RunPlusPlus.Model;
using RunPlusPlus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunPlusPlus.ViewModel
{
    class ShortcutViewModel : ViewModelBase
    {

        public ShortcutViewModel()
        {
            InitializeCommand();
            this.sysShortcut = new Shortcut();
        }

        private void InitializeCommand()
        {
            this.SaveCommand = new RelayCommand(this.Save, this.Check);
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.RaisePropertyChanged();
            }
        }

        private string _shortcut;

        public string Shortcut
        {
            get { return _shortcut; }
            set
            {
                _shortcut = value;
                this.RaisePropertyChanged();
            }
        }

        private string target;

        public string Target
        {
            get { return target; }
            set
            {
                target = value;
                this.RaisePropertyChanged();
            }
        }

        private string _startupPath;

        public string StartupPath
        {
            get { return _startupPath; }
            set
            {
                _startupPath = value;
                this.RaisePropertyChanged();
            }
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

        private WindowTypes _windowType = WindowTypes.Normal;

        public WindowTypes WindowType
        {
            get { return _windowType; }
            set
            {
                _windowType = value;
                this.RaisePropertyChanged();
            }
        }


        private Shortcut sysShortcut;

        public RelayCommand SaveCommand { get; set; }

        private void Save()
        {
            if (this.Check())
            {
                this.sysShortcut.Save();
            }
            else
            {
                throw new ArgumentException();
            }

        }

        private bool Check()
        {
            this.sysShortcut.Name = this.Shortcut;
            this.sysShortcut.Description = this.Name;
            this.sysShortcut.Target = this.Target;
            this.sysShortcut.StartupPath = this.StartupPath;
            return this.sysShortcut.Check();
        }

        public void Delete()
        {
            this.sysShortcut.Delete();
        }
    }
}
