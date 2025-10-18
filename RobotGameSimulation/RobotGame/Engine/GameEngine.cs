using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotGameSimulation.RobotGame.Models;

namespace RobotGameSimulation.RobotGame.Engine
{
    public class GameEngine
    {
        private readonly Board _board;
        private readonly CommandParser _commandParser;
        public GameEngine()
        {
            _board = new Board();
            _commandParser = new CommandParser();
        }
        public void Run()
        {
            Console.WriteLine("=== Toy Robot Game ===");
            while (true)
            {
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    continue;
                ExecuteCommand(input);
            }
        }
        private void ExecuteCommand(string input)
        {
            try
            {
                var command = _commandParser.Parse(input);

                if (command == null)
                {
                    return;
                }

                command.Execute(_board);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
