namespace RunPlusPlus.Model
{
    class Shortcut
    {

        public Shortcut()
        {
            WindowType = WindowTypes.Normal;
        }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public string Arguments { get; set; }
        public string StartupPath { get; set; }
        public WindowTypes WindowType { get; set; }
    }
}
