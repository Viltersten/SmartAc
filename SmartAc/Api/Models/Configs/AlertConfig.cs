using System.Collections.Generic;

namespace Api.Models.Configs
{
    public class AlertConfig : Dictionary<string, AlertConfig.AlertDetail>
    {
        //public string Name { get; set; }
        //public Dictionary<string, AlertDetail> Details { get; set; }

        public class AlertDetail
        {
            public string Name { get; set; }
            public string Message { get; set; }
            public double Max { get; set; }
            public double Min { get; set; }
        }
    }
}
