using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using RobotGameSimulation.RobotGame.Engine;
using Xunit;

namespace RobotGameSimulation.Tests.RobotGameTests.Integration
{
    public class GameIntegrationTests
    {
        [Fact]
        public void PlaceRobot_ValidPosition_Success()
        {
            var gameEngine = new GameEngine();
            var sw = new StringWriter();
            Console.SetOut(sw);

            gameEngine.ExecuteCommand("PLACE_ROBOT 2,3,NORTH");
            gameEngine.ExecuteCommand("REPORT");

            var result = sw.ToString();
            Assert.Contains("2,3,NORTH", result);
        }

        [Fact]
        public void PlaceRobot_InvalidPosition()
        {
            var gameEngine = new GameEngine();
            var sw = new StringWriter();
            Console.SetOut(sw);

            gameEngine.ExecuteCommand("PLACE_ROBOT 2,3,NORTH");
            //should ignore this - invlalid position
            gameEngine.ExecuteCommand("PLACE_ROBOT 10,10,NORTH");
            gameEngine.ExecuteCommand("REPORT");

            var result = sw.ToString();
            Assert.Contains("2,3,NORTH", result);

            //should ignore this - wall in place
            gameEngine.ExecuteCommand("PLACE_WALL 3,3");
            gameEngine.ExecuteCommand("PLACE_ROBOT 2,3,NORTH");
            gameEngine.ExecuteCommand("REPORT");

            result = sw.ToString();
            Assert.Contains("2,3,NORTH", result);
        }

        [Fact]
        public void RotateLeft_UpdateFacing_Success()
        {
            var engine = new GameEngine();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            engine.ExecuteCommand("PLACE_ROBOT 3,3,NORTH");
            engine.ExecuteCommand("LEFT");
            engine.ExecuteCommand("REPORT");

            var output = sw.ToString().Trim();
            Assert.Contains("3,3,WEST", output);

            engine.ExecuteCommand("LEFT");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("3,3,SOUTH", output);

            engine.ExecuteCommand("LEFT");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("3,3,EAST", output);
        }

        [Fact]
        public void RotateRight_UpdateFacing_Success()
        {
            var engine = new GameEngine();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            engine.ExecuteCommand("PLACE_ROBOT 3,3,NORTH");
            engine.ExecuteCommand("RIGHT");
            engine.ExecuteCommand("REPORT");

            var output = sw.ToString().Trim();
            Assert.Contains("3,3,EAST", output);

            engine.ExecuteCommand("RIGHT");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("3,3,SOUTH", output);

            engine.ExecuteCommand("RIGHT");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("3,3,WEST", output);
        }

        [Fact]
        public void MoveRobot_ValidMoves_Success()
        {
            var engine = new GameEngine();
            using var sw = new StringWriter();
            Console.SetOut(sw);
            
            // Test moving
            engine.ExecuteCommand("PLACE_ROBOT 1,1,NORTH");
            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("REPORT");

            var output = sw.ToString().Trim();
            Assert.Contains("2,1,NORTH", output);

            // Test turning right and moving
            engine.ExecuteCommand("RIGHT");
            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("2,2,EAST", output);

            // Test turning left and moving
            engine.ExecuteCommand("LEFT");
            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("3,2,NORTH", output);

            // Test moving thought the edge
            engine.ExecuteCommand("PLACE_ROBOT 1,1,SOUTH");
            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("5,1,SOUTH", output);
        }

        [Fact]
        public void MoveRobot_InvalidMoveBecauseOfWall()
        {
            var engine = new GameEngine();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Test moving into a wall
            engine.ExecuteCommand("PLACE_ROBOT 1,1,EAST");
            engine.ExecuteCommand("PLACE_WALL 1,2");
            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("REPORT");

            var output = sw.ToString().Trim();
            Assert.Contains("1,1,EAST", output);

            // Test moving thought the edge into wall
            engine.ExecuteCommand("RIGHT");
            engine.ExecuteCommand("PLACE_WALL 5,1");
            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("REPORT");

            output = sw.ToString().Trim();
            Assert.Contains("1,1,SOUTH", output);
        }

        [Fact]
        public void Commands_InvalidNoPlacement()
        {
            var engine = new GameEngine();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            engine.ExecuteCommand("MOVE");
            engine.ExecuteCommand("LEFT");
            engine.ExecuteCommand("RIGHT");
            engine.ExecuteCommand("REPORT");

            var output = sw.ToString().Trim();
            Assert.Empty(output);
        }
    }
}
