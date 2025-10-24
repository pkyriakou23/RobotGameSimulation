using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Enums;

namespace RobotGameSimulation.RobotGame.Models
{
    public class Robot
    {
        public Position? Position { get; private set; }
        public FacingDirection FacingDirection { get; private set; }
        public bool IsPlaced => Position != null;

        public Robot(Position position, FacingDirection facing)
        {
            Position = position;
            FacingDirection = facing;
        }
        public void PlaceRobot(Position position, FacingDirection facing)
        {
            Position = position;
            FacingDirection = facing;
        }
        public void TurnRight()
        {
            if (!IsPlaced) return;

            FacingDirection = FacingDirection switch
            {
                FacingDirection.NORTH => FacingDirection.EAST,
                FacingDirection.EAST => FacingDirection.SOUTH,
                FacingDirection.SOUTH => FacingDirection.WEST,
                FacingDirection.WEST => FacingDirection.NORTH,
                _ => FacingDirection
            };
        }
        public void TurnLeft()
        {
            if (!IsPlaced) return;

            FacingDirection = FacingDirection switch
            {
                FacingDirection.NORTH => FacingDirection.WEST,
                FacingDirection.WEST => FacingDirection.SOUTH,
                FacingDirection.SOUTH => FacingDirection.EAST,
                FacingDirection.EAST => FacingDirection.NORTH,
                _ => FacingDirection
            };
        }
        public string? Report() => IsPlaced ? $"{Position},{FacingDirection}" : null;
        public void MoveTo(Position newPosition)
        {
            if (!IsPlaced) return;

            Position = newPosition;
        }
    }
}
