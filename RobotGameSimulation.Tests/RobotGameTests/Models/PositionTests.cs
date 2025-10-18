using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Models;
using Xunit;

namespace RobotGameSimulation.Tests.RobotGameTests.Models
{
    public class PositionTests
    {
        [Fact]
        public void Positions_BeEqual()
        {
            var pos1 = new Position(2, 3);
            var pos2 = new Position(2, 3);

            Assert.Equal(pos1, pos2);
            Assert.True(pos1.Equals(pos2));
            Assert.Equal(pos1.GetHashCode(), pos2.GetHashCode());
        }

        [Fact]
        public void Positions_NotBeEqual()
        {
            var pos1 = new Position(1, 2);
            var pos2 = new Position(2, 1);

            Assert.NotEqual(pos1, pos2);
        }

        [Fact]
        public void ToString_CorrectFormat()
        {
            var pos = new Position(4, 5);
            Assert.Equal("4,5", pos.ToString());
        }
    }
}
