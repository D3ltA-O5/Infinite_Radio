using Exiled.API.Interfaces;
using System.ComponentModel;

namespace InfiniteRadio
{
    public class Config : IConfig
    {
        [Description("Is the plugin enebled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should it send debug log messages?")]
        public bool Debug { get; set; } = false;
    }
}
