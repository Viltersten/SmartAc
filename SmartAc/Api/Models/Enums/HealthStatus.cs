using System.ComponentModel;

namespace Api.Models.Enums
{
    public enum HealthStatus
    {
        [Description("None")]
        None = 0,
        [Description("OK")]
        Ok,
        [Description("needs_filter")]
        Filter,
        [Description("needs_service")]
        Service
    }
}
