using System;

namespace TDDWrestle
{
    public class Wrestler
    {
        public Wrestler()
        {
            WrestlerId = Guid.NewGuid();
        }

        public Guid WrestlerId { get; set; }
        public int Points { get; set; }
        public int TechnicalViolations { get; set; }
        public uint WeightClass { get; set; }
        
    }
}