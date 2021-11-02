using System;

namespace Api.Models.Misc
{
    public class Intersector
    {
        public DateTime? StartOn { get; set; }
        public DateTime? EndOn { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

        public int Offset => Page * Size;
    }
}
