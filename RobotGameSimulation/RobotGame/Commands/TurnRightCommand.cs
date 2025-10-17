using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.RobotGame.Commands
{
    public class TurnRightCommand : ICommand
    {
        public void Execute(Board board)
        {
            board.TurnRobotRight();
        }
    }
}
