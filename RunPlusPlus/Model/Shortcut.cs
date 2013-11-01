using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunPlusPlus.Model
{
    class Shortcut
    {

        public Shortcut()
        {
            this.WindowType = WindowTypes.Normal;
        }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public string StartupPath { get; set; }
        public WindowTypes WindowType { get; set; }
    }
}
