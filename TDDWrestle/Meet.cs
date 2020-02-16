using System;
using System.ComponentModel.Design;

namespace TDDWrestle
{
    internal class Meet
    {
        public Meet(Team homeTeamId, Team visitingTeamId, DateTime date)
        {
            MeetId = Guid.NewGuid();
            Complete = false;
            HomeTeam = homeTeamId;
            VisitingTeam = visitingTeamId;
            Date = date;
        }

        public Guid MeetId { get; set; }

        public Team HomeTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public Team VisitingTeam { get; set; }
        public int VistingTeamScore { get; set; }
        public bool Complete { get; set; }
        public Team Winner { get; set; }
        public DateTime Date { get; set; }

        public Team GetWinner()
        {
            int result = HomeTeamScore - VistingTeamScore;
            if (result > 0)
            {
                return HomeTeam;
            }
            else if (result < 0)
            {
                return VisitingTeam;
            }

            return null;
        }
    }
}