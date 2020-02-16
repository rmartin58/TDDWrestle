using System;
using FluentAssertions;
using Xunit;

namespace TDDWrestle.Tests
{
    public class MatchTests
    {
        private Match _match;

        [Fact]
        public void Match_ShouldHaveId()
        {
            GivenAMatch();
            WhenMatchStarted();
            ThenMatch_ShouldHaveId();
            ThenMatch_IsStarted();
        }


        [Fact]
        public void PendingMatch_ShouldHavePendingStatus()
        {
            GivenAMatch();
            ThenMatch_IsPending();
        }

        private void ThenMatch_IsPending()
        {
            _match.Status.Should().Be("Pending");
        }


        private void WhenMatchStarted()
        {
            _match.Status = "Active";
        }

        private void ThenMatch_IsStarted()
        {
            _match.Status.Should().Be("Active");
        }

        private void ThenMatch_ShouldHaveId()
        {
            _match.MatchId.GetType().Should().Be(typeof(Guid));
            _match.MatchId.ToString().Should().NotBeNull();
            Console.WriteLine(_match.MatchId.ToString());
        }

        private void GivenAMatch()
        {
            _match = new Match {MatchId = Guid.NewGuid(), Status = "Pending"};
        }
    }
}
