using Battleship.Core.Game;
using Battleship.Core.Game.Services;

namespace Battleship.ConsoleApplication;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to a battleship game!");
        Console.WriteLine("There is 1 battleship and 2 destroyers.");
        Console.WriteLine("Please enter the coordinates in the form \"B1\", where B is a column and 1 is a row.");
        Console.WriteLine("The game will end after all the ships are destroyed.");
        
        var engine = new GameEngine(new BattleshipConsoleInput(), new BattleshipConsoleOutput(), new ShipPositionGenerator(), new ShipFactory(ShipsConfig.Default));
        engine.StartGame();
        
        Console.WriteLine("Press enter to finish.");
        Console.ReadLine();
    }
}