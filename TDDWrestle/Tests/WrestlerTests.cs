using System;
using FluentAssertions;
using Xunit;

namespace TDDWrestle.Tests
{
    public class WrestlerTests
    {
        private Wrestler _wrestler;
        private Wrestler _opponent;
        private Match _match;


        [Fact]
        public void AWrestlerHasUniqueId()
        {
            GivenAWrestler(220);
            ThenWrestlerShouldHaveId();
        }

        [Fact]
        public void WrestlerIsAwardedATakeDown_ShouldHave2Points()
        {
            GivenAWrestler(106);
            WhenWrestlerTakesDownOpponent();
            ThenWrestlerHasPoints(2);
        }

        [Fact]
        public void WrestlerIsAwarded3TakeDowns_ShouldHave6Points()
        {
            GivenAWrestler(113);
            WhenWrestlerTakesDownOpponent();
            WhenWrestlerTakesDownOpponent();
            WhenWrestlerTakesDownOpponent();
            ThenWrestlerHasPoints(6);
        }


        [Fact]
        public void WrestlerAwardedReversal_ShouldHave2Points()
        {
            GivenAWrestler(140);
            WhenWrestlerReversesOpponent();
            ThenWrestlerHasPoints(2);
        }


        [Fact]
        public void WrestlerEscapes_ShouldHave1Point()
        {
            GivenAWrestler(126);
            WhenWrestlerEscapesOpponent();
            ThenWrestlerHasPoints(1);
        }

        [Fact]
        public void WrestlerAwarded4PointNearFall_ShouldHave4Points()
        {
            GivenAWrestler(220);
            WhenWrestlerIsAwardedNearFall(4);
            ThenWrestlerHasPoints(4);
        }

        [Fact]
        public void WrestlerAwarded3PointNearFall_ShouldHave3Points()
        {
            GivenAWrestler(285);
            WhenWrestlerIsAwardedNearFall(3);
            ThenWrestlerHasPoints(3);
        }
        
        [Fact]
        public void WrestlerAwarded2PointNearFall_ShouldHave2points()
        {
            GivenAWrestler(185);
            WhenWrestlerIsAwardedNearFall(2);
            ThenWrestlerHasPoints(2);
        }

        [Fact]
        public void WrestlerPinsHisOppponent_ShouldWinTheMatch()
        {
            GivenAWrestler(195);
            GivenAnOpponent();
            GivenAMatch(_wrestler, _opponent);
            WhenWrestlerPinsOpponent();
            ThenWrestlerWinsTheMatch();
            ThenWrestlerWinsBy("Pin");
        }


        [Fact]
        public void WrestlerOutscoresOpponentByLessThan10Points_ShouldWinByDecision()
        {
            GivenAWrestler(160);
            GivenAnOpponent();
            GivenAMatch(_wrestler, _opponent);
            GivenWrestlerOutscoresOpponentByLessThanTenPoints();
            WhenMatchEnds();
            ThenWrestlerWinsBy("Decision");
        }


        [Fact]
        public void WrestlerOutscoresOpponentByMoreThan10Points_ShouldWinByDecision()
        {
            GivenAWrestler(150);
            GivenAnOpponent();
            GivenAMatch(_wrestler, _opponent);
            GivenWrestlerOutscoresOpponentByMoreThanTenPoints();
            WhenMatchEnds();
            ThenWrestlerWinsBy("Major Decision");
        }

        [Fact]
        public void OpponentChargedWithTechnicalVioaltion_Wrestler_ShouldHave1Point()
        {
            GivenAWrestler(138);
            GivenAnOpponent();
            WhenOponentIsPenalized();
            ThenWrestlerHasPoints(1);
            ThenOponentHasTechnicalViolations(1);
        }

        [Fact]
        public void OpponentChargedWith2TechnicalViolations_Wrestler_ShouldHave3Points()
        {
            GivenAWrestler(145);
            GivenAnOpponent();
            WhenOponentIsPenalized();
            WhenOponentIsPenalized();
            ThenWrestlerHasPoints(3);
            ThenOponentHasTechnicalViolations(2);
        }

        [Fact]
        public void OpponentChargedWith3TechnicalViolations_Wrestler_ShouldWinByForfeit()
        {
            GivenAWrestler(165);
            GivenAnOpponent();
            GivenAMatch(_wrestler, _opponent);
            WhenOponentIsPenalized();
            WhenOponentIsPenalized();
            WhenOponentIsPenalized();
            ThenWrestlerWinsTheMatch();
            ThenWrestlerWinsBy("Forfeit");
        }

        [Fact]
        public void WrestlerOpponentForfeits_ShouldWinByForfeit()
        {
            GivenAWrestler(175);
            GivenAMatch(_wrestler,_opponent);
            WhenMatchForfeited();
            ThenWrestlerWinsTheMatch();
            ThenWrestlerWinsBy("Forfeit");
        }

        private void WhenMatchForfeited()
        {
            _match.Winner = _wrestler;
            _match.WonBy = "Forfeit";
        }


        private void WhenWrestlerEscapesOpponent()
        {
            _wrestler.Points += 1;
        }


        private void WhenWrestlerIsAwardedNearFall(int nearFallPoints)
        {
            _wrestler.Points += nearFallPoints;
        }

        private void WhenWrestlerReversesOpponent()
        {
            _wrestler.Points += 2;
        }


        private void ThenOponentHasTechnicalViolations(int technicalViolationCount)
        {
            _opponent.TechnicalViolations.Should().Be(technicalViolationCount);
        }

        private void WhenOponentIsPenalized()
        {
            _opponent.TechnicalViolations += 1;
            if (_opponent.TechnicalViolations > 2)
            {
                if (_match != null)
                {
                    _match.Winner = _wrestler;
                    _match.WonBy = "Forfeit";
                }
            }
            else if(_opponent.TechnicalViolations == 2)
            {
                _wrestler.Points += 2;
            }
            else
            {
                _wrestler.Points += 1;
            }
        }


        private void ThenWrestlerWinsTheMatch()
        {
            _match.Winner.Should().Equals(_wrestler);
        }

        private void WhenWrestlerPinsOpponent()
        {
            if (_match != null)
            {
                _match.Winner = _wrestler;
                _match.WonBy = "Pin";
            }
        }

        private void ThenWrestlerWinsBy(string wonBy)
        {
            _match.WonBy.Should().BeEquivalentTo(wonBy);
        }


        private void GivenAMatch(Wrestler wrestler, Wrestler opponent)
        {
            _match = new Match {HomeWrestler = wrestler, VisitorWrestler = opponent, Round = 1};
        }

        private void GivenAnOpponent()
        {
            _opponent = new Wrestler();
        }

        private void ThenWrestlerHasPoints(int points)
        {
            _wrestler.Points.Should().Be(points);
        }

        private void WhenWrestlerTakesDownOpponent()
        {
            _wrestler.Points += 2;
        }

        private void GivenAWrestler(uint weightClass)
        {
            _wrestler = new Wrestler {WeightClass = weightClass};
        }

        
        private void WhenMatchEnds()
        {
            int pointDifferential = _wrestler.Points - _opponent.Points;
            _match.Winner = pointDifferential > 0 ? _wrestler : _opponent;
            _match.WonBy = Math.Abs(pointDifferential) > 10 ? "Major Decision" : "Decision";
        }

        private void GivenWrestlerOutscoresOpponentByLessThanTenPoints()
        {
            _wrestler.Points = 5;
            _opponent.Points = 1;
        }

        private void GivenWrestlerOutscoresOpponentByMoreThanTenPoints()
        {
            _wrestler.Points = 12;
            _opponent.Points = 1;
        }

        private void ThenWrestlerShouldHaveId()
        {
            _wrestler.WrestlerId.GetType().Should().Be(typeof(Guid));

        }

    }
}
