using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.RobotGame.Commands
{
    public class PlaceWallCommand : ICommand
    {
        public int Row { get; }
        public int Col { get; }

        public PlaceWallCommand(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void Execute(Board board)
        {
            board.PlaceWall(new Position(Row, Col));
        }
    }
}
