using RobotGameSimulation.RobotGame.Engine;

namespace RobotGameSimulation.RobotGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new GameEngine();
            gameEngine.Run();
        }
    }
}