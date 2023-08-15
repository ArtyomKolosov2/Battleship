using Battleship.Core.Input;
using Battleship.Core.ValueObjects;
using Battleship.Shared;

namespace Battleship.ConsoleApplication;

public class BattleshipConsoleInput : IBattleshipInput
{
    public Coordinates GetCoordinatesFromUser()
    {
        Console.WriteLine("Please input coordinates:");
        Coordinates? parsedCoordinates = null;
        
        while (parsedCoordinates is null)
        {
            var input = Console.ReadLine();
            var parseResult = ParseCoordinatesFromUserInput(input);
            
            if (parseResult.IsSuccess)
                parsedCoordinates = parseResult.Data;
            else
                Console.WriteLine(parseResult.Error);
        }

        return parsedCoordinates;
    }

    private static Result<Coordinates, string> ParseCoordinatesFromUserInput(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Result<Coordinates, string>.Failure("Provided input is empty.");
        
        if (input.Length < 2)
            return Result<Coordinates, string>.Failure("Provided string is shorter then expected.");
        
        input = input.ToLower();
        
        var columnChar = input[0];
        var rowString = input[1..];

        var isColumnCharInAllowedRange = columnChar is >= 'a' and <= 'j';
        if (!isColumnCharInAllowedRange)
            return Result<Coordinates, string>.Failure("Invalid column symbol.");
        
        var column = Math.Abs('a' - columnChar);
        var isRowCanBeParsedSuccessfully = rowString.All(char.IsDigit);

        if (!isRowCanBeParsedSuccessfully)
            return Result<Coordinates, string>.Failure("Row isn't a number.");

        var rowValue = int.Parse(rowString) - 1;

        return rowValue switch
        {
            > 10 => Result<Coordinates, string>.Failure("Row number exceeds max allowed rows."),
            < 0 => Result<Coordinates, string>.Failure("Row number is under min first row."),
            _ => Result<Coordinates, string>.Success(new Coordinates(rowValue, column))
        };
    }
}