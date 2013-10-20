using GalaSoft.MvvmLight;
using RunPlusPlus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunPlusPlus.ViewModel
{
    class ShortcutViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        private string _shortcut;

        public string Shortcut
        {
            get { return _shortcut; }
            set
            {
                _shortcut = value;
                this.RaisePropertyChanged("Shortcut");
            }
        }

        private string _command;

        public string Command
        {
            get { return _command; }
            set
            {
                _command = value;
                this.RaisePropertyChanged("Command");
            }
        }

        private bool _isRunAsAdministrstor;

        public bool IsRunAsAdministrator
        {
            get { return _isRunAsAdministrstor; }
            set
            {
                _isRunAsAdministrstor = value;
                this.RaisePropertyChanged("IsRunAsAdministrator");
            }
        }

        private WindowType _windowType;

        public WindowType WindowType
        {
            get { return _windowType; }
            set
            {
                _windowType = value;
                this.RaisePropertyChanged("WindowType");
            }
        }

        public void Save()
        {

        }



        public void Delete()
        {

        }
    }
}
