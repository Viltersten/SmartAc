using System;

namespace Api.Models.Domain
{
    public class Device
    {
        public string Id { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public DateTime InitedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string Version => $"{Major}.{Minor}.{Patch}";
    }
}