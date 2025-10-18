using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Commands;
using RobotGameSimulation.RobotGame.Enums;

namespace RobotGameSimulation.RobotGame.Engine
{
    public class CommandParser
    {
        public ICommand? Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var parts = input.Trim().Split(' ');
            var commandType = parts[0].ToUpper();

            return commandType switch
            {
                "PLACE_ROBOT" => ParsePlaceRobotCommand(parts),
                "PLACE_WALL" => ParsePlaceWallCommand(parts),
                "MOVE" => new MoveCommand(),
                "LEFT" => new TurnLeftCommand(),
                "RIGHT" => new TurnRightCommand(),
                "REPORT" => new ReportCommand(),
                _ => null
            };
        }
        private ICommand? ParsePlaceRobotCommand(string[] parts)
        {
            if (parts.Length < 2) return null;

            var arguments = parts[1].Split(',');
            if (arguments.Length != 3) return null;

            if (!int.TryParse(arguments[0], out int row) ||
                !int.TryParse(arguments[1], out int col) ||
                !Enum.TryParse<FacingDirection>(arguments[2].ToUpper(), out var facing))
            {
                return null;
            }

            return new PlaceRobotCommand(row, col, facing);
        }


            private ICommand? ParsePlaceWallCommand(string[] parts)
        {
            if (parts.Length < 2) return null;

            var arguments = parts[1].Split(',');
            if (arguments.Length != 2) return null;

            if (!int.TryParse(arguments[0], out int row) ||
                !int.TryParse(arguments[1], out int col))
            {
                return null;
            }

            return new PlaceWallCommand(row, col);
        }
    }
}
