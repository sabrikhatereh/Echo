using System.Collections.Generic;

namespace Echo.Core.Configuration
{
    public class ValidationSettings
    {
        public List<string> InvalidWords { get; set; } = new();
    }
}
