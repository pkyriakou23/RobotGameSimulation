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

            var parts = input.Trim().Split(' ', 2);
            if (!Enum.TryParse<CommandType>(parts[0], true, out var commandType))
                return null;

            return commandType switch
            {
                CommandType.PLACE_ROBOT => ParsePlaceRobotCommand(parts),
                CommandType.PLACE_WALL => ParsePlaceWallCommand(parts),
                CommandType.MOVE => new MoveCommand(),
                CommandType.LEFT => new TurnLeftCommand(),
                CommandType.RIGHT => new TurnRightCommand(),
                CommandType.REPORT => new ReportCommand(),
                _ => null
            };
        }
        private ICommand? ParsePlaceRobotCommand(string[] parts)
        {
            if (parts.Length < 2) return null;

            var arguments = SplitArguments(parts.Length > 1 ? parts[1] : null, 3);
            if (arguments == null) return null;

            if (!int.TryParse(arguments[0], out int row) ||
                !int.TryParse(arguments[1], out int col) ||
                !Enum.TryParse<FacingDirection>(arguments[2], true, out var facing))
            {
                return null;
            }

            return new PlaceRobotCommand(row, col, facing);
        }
        private ICommand? ParsePlaceWallCommand(string[] parts)
        {
            if (parts.Length < 2) return null;

            var arguments = SplitArguments(parts.Length > 1 ? parts[1] : null, 2);
            if (arguments == null) return null;

            if (!int.TryParse(arguments[0], out int row) ||
                !int.TryParse(arguments[1], out int col))
            {
                return null;
            }

            return new PlaceWallCommand(row, col);
        }
        private string[]? SplitArguments(string? argString, int expectedCount)
        {
            if (string.IsNullOrWhiteSpace(argString)) return null;

            var args = argString.Split(',', StringSplitOptions.TrimEntries);
            return args.Length == expectedCount ? args : null;
        }
    }
}
