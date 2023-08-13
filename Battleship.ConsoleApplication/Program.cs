using Battleship.Core.Game;

namespace Battleship.ConsoleApplication;

public static class Program
{
    public static void Main()
    {
        var game = new GameEngine(new BattleshipConsoleInput(), new BattleshipConsoleOutput());
        game.StartGame();
    }
}

