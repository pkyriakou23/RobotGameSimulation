using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Enums;

namespace RobotGameSimulation.RobotGame.Models
{
    public class Board
    {
        private const int BoardSize = 5;
        public Robot? Robot { get; private set; }
        private readonly HashSet<Position> _walls;
        public Board()
        {
            Robot = null;
            _walls = new HashSet<Position>();
        }
        public void PlaceRobot(Position position, FacingDirection facing)
        {
            if (!IsPositionValid(position)) return;

            if (IsWall(position)) return;

            if (Robot == null)
            {
                Robot = new Robot(position, facing);
            }
            else
            {
                Robot.PlaceRobot(position, facing);
            }
        }
        public void PlaceWall(Position position)
        {
            if (!IsPositionValid(position)) return;

            if (IsWall(position) || IsRobotAt(position)) return;

            _walls.Add(position);
        }
        public void MoveRobot()
        {
            if (Robot == null || !Robot.IsPlaced) return;

            var currentPosition = Robot.Position!;
            var newPosition = CalculateNewPosition(currentPosition, Robot.FacingDirection);

            if (_walls.Contains(newPosition)) return;

            Robot.MoveTo(newPosition);
        }

        public void TurnRobotLeft()
        {
            if (Robot == null || !Robot.IsPlaced) return;
            Robot.TurnLeft();
        }

        public void TurnRobotRight()
        {
            if (Robot == null || !Robot.IsPlaced) return;
            Robot.TurnRight();
        }
        private Position CalculateNewPosition(Position current, FacingDirection facing)
        {
            return facing switch
            {
                FacingDirection.NORTH => new Position(current.Row + 1, current.Col),
                FacingDirection.EAST => new Position(current.Row, current.Col + 1),
                FacingDirection.SOUTH => new Position(current.Row - 1, current.Col),
                FacingDirection.WEST => new Position(current.Row, current.Col - 1),
                _ => current
            };
        }
        private bool IsPositionValid(Position position)
        {
            return position.Row >= 1 && position.Row <= BoardSize
                && position.Col >= 1 && position.Col <= BoardSize;
        }
        private bool IsWall(Position position) => _walls.Contains(position);
        private bool IsRobotAt(Position position) => Robot != null && Robot.IsPlaced && Robot.Position!.Equals(position);

        public string? Report()
        {
            if (Robot == null || !Robot.IsPlaced) return null;
            return Robot.Report();
        }
    }
}
