using Battleship.Core.Input;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Common;

namespace Battleship.ConsoleApplication;

public class BattleshipConsoleInput : IBattleshipInput
{
    public Coordinates GetCoordinatesFromUser()
    {
        Console.WriteLine("Please input coordinates:");
        string? input = null;
        Coordinates? parsedCoordinates = null;
        
        while (input is null && parsedCoordinates is null)
        {
            input = Console.ReadLine();
            parsedCoordinates = input is not null ? ParseCoordinatesFromUserInput(input) : null;
        }

        return parsedCoordinates!;
    }

    private static Coordinates? ParseCoordinatesFromUserInput(string input)
    {
        input = input.ToLower();
        
        var columnChar = input[0];
        var rowChar = input[1];

        var column = Math.Abs('a' - columnChar);
        var isRowCanBeParsedSuccessfully = char.IsDigit(rowChar);

        if (!isRowCanBeParsedSuccessfully)
            return null;

        return new Coordinates((int)char.GetNumericValue(rowChar) - 1, column);
    }
}