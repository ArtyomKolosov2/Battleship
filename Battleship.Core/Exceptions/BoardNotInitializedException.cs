namespace Battleship.Core.Exceptions;

public class BoardNotInitializedException : Exception
{
    public BoardNotInitializedException(string message) : base(message)
    {
    }
    
    public static void Throw() => throw new BoardNotInitializedException("Panels must be initialized on the board.");
}