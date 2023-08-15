using Battleship.Core.Game;
using Battleship.Core.Game.Services;

namespace Battleship.ConsoleApplication;

public static class Program
{
    public static void Main()
    {
        var engine = new GameEngine(new BattleshipConsoleInput(), new BattleshipConsoleOutput(), new ShipPositionGenerator(), new ShipFactory(ShipsConfig.Default));
        engine.StartGame();
    }
}

