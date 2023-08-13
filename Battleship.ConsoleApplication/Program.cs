using Battleship.Core.Game;

namespace Battleship.ConsoleApplication;

public static class Program
{
    public static void Main()
    {
        var engine = new GameEngine(new BattleshipConsoleInput(), new BattleshipConsoleOutput());
        engine.StartGame();
    }
}

