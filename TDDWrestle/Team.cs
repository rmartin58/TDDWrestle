using System;

namespace TDDWrestle
{
    public class Team
    {
        public Team()
        {
            TeamId = Guid.NewGuid();
        }
        public Guid TeamId { get; set; }
    }
}