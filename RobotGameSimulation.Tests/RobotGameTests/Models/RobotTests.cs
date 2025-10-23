using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Enums;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.Tests.RobotGameTests.Models
{
    public class RobotTests
    {
        [Fact]
        public void PlaceRobot_SetPositionAndDirection()
        {
            var robot = new Robot(new Position(1, 1), FacingDirection.NORTH);

            Assert.True(robot.IsPlaced);
            Assert.Equal(1, robot.Position!.Row);
            Assert.Equal(1, robot.Position.Col);
            Assert.Equal(FacingDirection.NORTH, robot.FacingDirection);

            robot = new Robot(new Position(2, 5), FacingDirection.SOUTH);

            Assert.True(robot.IsPlaced);
            Assert.Equal(2, robot.Position!.Row);
            Assert.Equal(5, robot.Position.Col);
            Assert.Equal(FacingDirection.SOUTH, robot.FacingDirection);
        }

        [Fact]
        public void TurnRight_Rotate()
        {
            var robot = new Robot(new Position(2, 2), FacingDirection.NORTH);
            robot.TurnRight();
            Assert.Equal(FacingDirection.EAST, robot.FacingDirection);
        }

        [Fact]
        public void TurnLeft_Rotate()
        {
            var robot = new Robot(new Position(2, 2), FacingDirection.NORTH);
            robot.TurnLeft();
            Assert.Equal(FacingDirection.WEST, robot.FacingDirection);
        }

        [Fact]
        public void Report_CorrectFormat()
        {
            var robot = new Robot(new Position(3, 4), FacingDirection.SOUTH);
            var report = robot.Report();

            Assert.Equal("3,4,SOUTH", report);
        }

        [Fact]
        public void MoveTo_UpdatePosition()
        {
            var robot = new Robot(new Position(1, 4), FacingDirection.EAST);
            var newPos = new Position(1, 5);

            robot.MoveTo(newPos);

            Assert.Equal(newPos, robot.Position);
        }
    }
}
