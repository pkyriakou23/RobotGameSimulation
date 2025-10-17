using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.RobotGame.Commands
{
    public class ReportCommand : ICommand
    {
        public void Execute(Board board)
        {
            var report = board.Report();
            if (report != null)
            {
                Console.WriteLine(report);
            }
        }
}
