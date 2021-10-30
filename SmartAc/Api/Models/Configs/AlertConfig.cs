using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Configs
{
    public class AlertConfig : Dictionary<string, AlertConfig.AlertType>
    {
        //public string Name { get; set; }
        //public Dictionary<string, AlertType> Types { get; set; }

        public class AlertType
        {
            public string Name { get; set; }
            public string Message { get; set; }
            public double Max { get; set; }
            public double Min { get; set; }
        }
    }
}
