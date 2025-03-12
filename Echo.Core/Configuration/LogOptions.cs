using System.IO;

namespace Echo.Core.Configuration
{
    public class LogOptions
    {
        public string AppName { get; set; } = "Echo";
        public string Level { get; set; }
        public FileOptions File { get; set; }
        public string LogTemplate { get; set; }
    }
}
