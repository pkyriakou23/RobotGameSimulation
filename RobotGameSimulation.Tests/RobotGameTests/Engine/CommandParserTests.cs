using System;
using System.Collections.Generic;
using RobotGameSimulation.RobotGame.Commands;
using RobotGameSimulation.RobotGame.Engine;
using RobotGameSimulation.RobotGame.Enums;

namespace RobotGameSimulation.Tests.RobotGameTests.Engine
{
    public class CommandParserTests
    {
        private readonly CommandParser _parser;

        public CommandParserTests()
        {
            _parser = new CommandParser();
        }

        [Theory]
        [InlineData("PLACE_ROBOT 1,2,NORTH", 1, 2, FacingDirection.NORTH)]
        [InlineData("place_robot 5,5,west", 5, 5, FacingDirection.WEST)]
        [InlineData("  PLACE_ROBOT   3,4,South   ", 3, 4, FacingDirection.SOUTH)]
        public void Parse_ValidInput_ReturnPlaceRobotCommand(
           string input, int expectedRow, int expectedCol, FacingDirection expectedFacing)
        {
            var result = _parser.Parse(input);
            var command = Assert.IsType<PlaceRobotCommand>(result);

            Assert.Equal(expectedRow, command.Row);
            Assert.Equal(expectedCol, command.Col);
            Assert.Equal(expectedFacing, command.Facing);
        }

        [Theory]
        [InlineData("PLACE_WALL 2,3", 2, 3)]
        [InlineData("place_wall 5,1", 5, 1)]
        public void Parse_ValidInput_ReturnPlaceWallCommand(string input, int expectedRow, int expectedCol)
        {
            var result = _parser.Parse(input);

            var command = Assert.IsType<PlaceWallCommand>(result);
            Assert.Equal(expectedRow, command.Row);
            Assert.Equal(expectedCol, command.Col);
        }
        
        [Theory]
        [InlineData("MOVE", typeof(MoveCommand))]
        [InlineData("LEFT", typeof(TurnLeftCommand))]
        [InlineData("RIGHT", typeof(TurnRightCommand))]
        [InlineData("REPORT", typeof(ReportCommand))]
        public void Parse_ValidInput_ReturnSimpleCommand(string input, Type expectedType)
        {
            var result = _parser.Parse(input);

            Assert.NotNull(result);
            Assert.IsType(expectedType, result);
        }

        [Theory]
        [InlineData("PLACE_ROBOT 2,2,INVALIDDIRECTION")] // invalid facing
        [InlineData("PLACE_ROBOT 1,2")] // Missing facing
        [InlineData("PLACE_ROBOT 2,abc,NORTH")] // invalid number
        [InlineData("PLACE_WALL abc,2")] // invalid number
        [InlineData("PLACE_ROBOT 1.5,2,NORTH")] //decimal number
        [InlineData("PLACE_ROBOT 1,2,NORTH,EXTRA")] // Extra parameter
        [InlineData("MOVE Extra")] //Extra parameter
        [InlineData("LEFT 123")] //Extra parameter
        [InlineData("RIGHT abc")] //Extra parameter
        [InlineData("REPORT REPORT")] //Extra parameter
        [InlineData("INVALID")] // invalid command
        public void Parse_InvalidInput_ReturnNull(string input)
        {
            var result = _parser.Parse(input);
            Assert.Null(result);
        }
    }

}
