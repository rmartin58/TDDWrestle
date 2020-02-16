using System;

namespace TDDWrestle
{
    internal class Match
    {
        public Match()
        {
            MatchId = Guid.NewGuid();
        }

        public Wrestler Winner { get; set; }
        public string WonBy { get; set; }
        public uint Round { get; set; }
        public Wrestler HomeWrestler { get; set; }
        public Wrestler VisitorWrestler { get; set; }
        public Guid MatchId { get; set; }
        public string Status { get; set; }
    }
}