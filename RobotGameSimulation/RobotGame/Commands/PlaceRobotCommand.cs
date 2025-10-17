using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Enums;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.RobotGame.Commands
{
    public class PlaceRobotCommand
    {
        public int Row { get; }
        public int Col { get; }
        public FacingDirection Facing { get; }

            public PlaceRobotCommand(int row, int col, FacingDirection facing)
            {
                Row = row;
                Col = col;
                Facing = facing;
            }

            public void Execute(Board board)
            {
                board.PlaceRobot(new Position(Row, Col), Facing);
            }
        }
    }
}
