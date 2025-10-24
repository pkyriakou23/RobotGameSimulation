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

        /// <summary>
        /// Places a wall at the specified position on the board if the position is valid,
        /// not already occupied by a wall, and not occupied by the robot. If the board
        /// becomes completely blocked by walls, the application exits.
        /// </summary>
        /// <param name="position">The position where the wall is to be placed.</param>
        public void PlaceWall(Position position)
        {
            if (!IsPositionValid(position)) return;

            if (IsWall(position) || IsRobotAt(position)) return;

            _walls.Add(position);
            if (_walls.Count >= BoardSize * BoardSize)
            {
                Console.WriteLine("No other command can be executed: the board is completely blocked by walls");
                Environment.Exit(1);
            }
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

        /// <summary>
        /// Calculates the new position after moving in the given direction from the current position.
        /// Implements board wrapping: when moving beyond an edge, the robot appears on the opposite side.
        /// - NORTH: Increases row, wraps from top (5) to bottom (1)
        /// - SOUTH: Decreases row, wraps from bottom (1) to top (5)  
        /// - EAST: Increases column, wraps from right (5) to left (1)
        /// - WEST: Decreases column, wraps from left (1) to right (5)
        /// </summary>
        /// <param name="current">The current position of the robot</param>
        /// <param name="facing">The direction the robot is facing</param>
        /// <returns>The new position after movement with edge wrapping applied</returns>
        private Position CalculateNewPosition(Position current, FacingDirection facing)
        {
            return facing switch
            {
                FacingDirection.NORTH => new Position(current.Row == BoardSize ? 1 : current.Row + 1, current.Col),
                FacingDirection.SOUTH => new Position(current.Row == 1 ? BoardSize : current.Row - 1, current.Col),
                FacingDirection.EAST => new Position(current.Row, current.Col == BoardSize ? 1 : current.Col + 1),
                FacingDirection.WEST => new Position(current.Row, current.Col == 1 ? BoardSize : current.Col - 1),
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
