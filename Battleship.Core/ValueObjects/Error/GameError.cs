namespace Battleship.Core.ValueObjects.Error;

public class GameError
{
    public string Message { get; init; }

    private GameError(string message)
    {
        Message = message;
    }

    public static GameError WithMessage(string message) => new(message);
}