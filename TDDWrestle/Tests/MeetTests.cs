using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;
namespace TDDWrestle.Tests
{
    public class MeetTests
    {
        private Team _homeTeam;
        private Team _visitingTeam;
        private Meet _meet;

        [Fact]
        public void Meets_ShouldHave2Teams()
        {
            GivenAMeet();
            ThenMeets_ShouldHave2Teams();
            ThenMeetScore_ShouldBe0To0();
        }

        [Fact]
        public void Meets_ShouldHaveADate()
        {
            GivenAMeet();
            ThenMeetDate_ShouldNotBeNull();
        }

        private void ThenMeetDate_ShouldNotBeNull()
        {
            _meet.Date.Should().BeOnOrBefore(DateTime.Now);
            Debug.WriteLine(_meet.Date);
        }


        [Fact]
        public void WhenHomeTeamWinsMatchByPin_ThenHomeTeam_ShouldHave6Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByPin();
            ThenHomeTeam_ShouldHave6Points();
        }

        [Fact]
        public void WhenHomeTeamWinsMatchByForfeit_ThenHomeTeam_ShouldHave6Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByForfeit();
            ThenHomeTeam_ShouldHave6Points();
        }

        [Fact]
        public void WhenHomeTeamWinsMatchByDisqualification_ThenHomeTeam_ShouldHave6Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByDisqualification();
            ThenHomeTeam_ShouldHave6Points();
        }

        [Fact]
        public void WhenHomeTeamWinsMatchByInjuryDefault_ThenHomeTeam_ShouldHave6Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByInjuryDefault();
            ThenHomeTeam_ShouldHave6Points();
        }

        [Fact]
        public void WhenHomeTeamWinsMatchByDecision_ThenHomeTeam_ShouldHave3Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByDecision();
            ThenHomeTeam_ShouldHave3Points();
        }

        [Fact]
        public void WhenHomeTeamWinsMatchByTechnicalFall_ThenHomeTeam_ShouldHave5Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByTechnicalFall();
            ThenHomeTeam_ShouldHave5Points();
        }

        [Fact]
        public void WhenHomeTeamWinsMatchByMajorDecision_ThenHomeTeam_ShouldHave4Points()
        {
            GivenAMeet();
            WhenHomeTeamWinsMatchByMajorDecision();
            ThenHomeTeam_ShouldHave4Points();
        }

        [Fact]
        public void WhenAllMatchesCompletedAndScoreIsEqual_ThenMeet_ShouldBeADraw()
        {
            GivenAMeet();
            WhenAllMatchesCompletedAndScoreIsEqual();
            ThenMeet_ShouldBeADraw();
        }

        [Fact]
        public void WhenAllMatchesCompletedAndHomeTeamOutscoresVisitor_ThenHomeTeam_ShouldWinTheMeet()
        {
            GivenAMeet();
            WhenAllMatchesCompletedAndHomeTeamOutscoresVisitor();
            ThenHomeTeam_ShouldWinTheMeet();
        }

        [Fact]
        public void WhenAllMatchesCompletedAndVisitingTeamOutscoresHomeTeam_ThenVisitingTeam_ShouldWinTheMeet()
        {
            GivenAMeet();
            WhenAllMatchesCompletedAndVisitingTeamOutscoresHomeTeam();
            ThenVisitingTeam_ShouldWinTheMeet();
        }

        private void ThenMeet_ShouldBeADraw()
        {
            _meet.Winner.Should().BeNull();
        }

        private void WhenAllMatchesCompletedAndScoreIsEqual()
        {
            _meet.Complete = true;
            _meet.VistingTeamScore = 20;
            _meet.HomeTeamScore = _meet.VistingTeamScore;
            _meet.GetWinner();
        }

        private void ThenVisitingTeam_ShouldWinTheMeet()
        {
            _meet.Winner.Should().BeEquivalentTo(_meet.VisitingTeam);
        }

        private void WhenAllMatchesCompletedAndVisitingTeamOutscoresHomeTeam()
        {
            _meet.Complete = true;
            _meet.VistingTeamScore = 20;
            _meet.HomeTeamScore = _meet.VistingTeamScore - 1;
            _meet.Winner = _meet.GetWinner();
        }

        private void ThenHomeTeam_ShouldWinTheMeet()
        {
            _meet.Winner.Should().BeEquivalentTo(_meet.HomeTeam);
        }

        private void WhenAllMatchesCompletedAndHomeTeamOutscoresVisitor()
        {
            _meet.Complete = true;
            _meet.VistingTeamScore = 20;
            _meet.HomeTeamScore = _meet.VistingTeamScore + 1;
            _meet.Winner = _meet.GetWinner();
        }

        private void WhenHomeTeamWinsMatchByInjuryDefault()
        {
            _meet.HomeTeamScore += 6;
        }

        private void ThenHomeTeam_ShouldHave4Points()
        {
            _meet.HomeTeamScore.Should().Be(4);
        }

        private void WhenHomeTeamWinsMatchByMajorDecision()
        {
            _meet.HomeTeamScore += 4;
        }

        private void ThenHomeTeam_ShouldHave5Points()
        {
            _meet.HomeTeamScore.Should().Be(5);
        }

        private void WhenHomeTeamWinsMatchByTechnicalFall()
        {
            _meet.HomeTeamScore += 5;
        }

        private void ThenHomeTeam_ShouldHave3Points()
        {
            _meet.HomeTeamScore.Should().Be(3);
        }

        private void WhenHomeTeamWinsMatchByDecision()
        {
            _meet.HomeTeamScore += 3;
        }

        private void WhenHomeTeamWinsMatchByForfeit()
        {
            _meet.HomeTeamScore += 6;
        }

        private void ThenHomeTeam_ShouldHave6Points()
        {
            _meet.HomeTeamScore.Should().Be(6);
        }

        private void WhenHomeTeamWinsMatchByPin()
        {
            _meet.HomeTeamScore += 6;
        }

        private void ThenMeets_ShouldHave2Teams()
        {
            this._homeTeam.Should().NotBeNull();
            _meet.VisitingTeam.Should().NotBeNull();
        }

        private void GivenAMeet()
        {
            _homeTeam = new Team();
            _visitingTeam = new Team();
            _meet = new Meet(_homeTeam, _visitingTeam, DateTime.Now);
        }

        private void ThenMeetScore_ShouldBe0To0()
        {
            _meet.HomeTeamScore.Should().Be(0);
            _meet.VistingTeamScore.Should().Be(0);
        }
        private void WhenHomeTeamWinsMatchByDisqualification()
        {
            _meet.HomeTeamScore += 6;
        }
    }
}
