using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Enums;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.Tests.RobotGameTests.Models
{
    public class BoardTests
    {
        [Fact]
        public void PlaceRobot_ValidPosition_PlaceRobot()
        {
            var board = new Board();
            var pos = new Position(2, 2);

            board.PlaceRobot(pos, FacingDirection.EAST);

            Assert.True(board.Robot!.IsPlaced);
            Assert.Equal(pos, board.Robot.Position);
            Assert.Equal(FacingDirection.EAST, board.Robot.FacingDirection);
        }

        [Fact]
        public void PlaceRobot_InvalidPosition()
        {
            var board = new Board();
            var pos = new Position(10, 2);

            board.PlaceRobot(pos, FacingDirection.EAST);

            Assert.Null(board.Robot);
        }

        [Fact]
        public void PlaceRobot_ValidPositionWithWall_PlaceRobot()
        {
            var board = new Board();
            var pos = new Position(2, 2);
            var wallPosition = new Position(3, 3);

            board.PlaceWall(wallPosition);
            board.PlaceRobot(pos, FacingDirection.EAST);

            Assert.True(board.Robot!.IsPlaced);
            Assert.Equal(pos, board.Robot.Position);
           Assert.NotEqual(wallPosition, board.Robot.Position);
        }

        /// <summary>
        /// Tests that PlaceRobot ignores placement when position is occupied by a wall.
        /// Also verifies that PlaceWall functionality works as expected.
        /// </summary>
        [Fact]
        public void PlaceRobot_InvalidPositionBecauseOfWall()
        {
            var board = new Board();
            var wallPosition = new Position(3, 3);

            board.PlaceWall(wallPosition);
            board.PlaceRobot(wallPosition, FacingDirection.EAST);

            Assert.Null(board.Robot);
        }

        [Fact]
        public void MoveRobot_UpdatePosition()
        {
            var board = new Board();
            var pos = new Position(1, 2);
            var newPos = new Position(1, 1);

            board.PlaceRobot(pos, FacingDirection.WEST);
            board.MoveRobot();

            Assert.Equal(newPos, board.Robot!.Position);

            newPos = new Position(1,5);
            board.MoveRobot();
            Assert.Equal(newPos, board.Robot!.Position);
        }

        [Fact]
        public void MoveRobot_InvalidPositionBecauseOfWall()
        {
            var board = new Board();
            var wallPosition = new Position(3, 3);
            var pos = new Position(2, 3);

            board.PlaceWall(wallPosition);
            board.PlaceRobot(pos, FacingDirection.NORTH);
            board.MoveRobot();

            Assert.Equal(pos, board.Robot!.Position);
        }

        [Fact]
        public void MoveRobot_InvalidRobotNotInPlace()
        {
            var board = new Board();
            board.MoveRobot();

            Assert.Null(board.Robot);
        }

    }
}
