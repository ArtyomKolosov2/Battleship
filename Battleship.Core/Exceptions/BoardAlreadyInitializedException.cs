namespace Battleship.Core.Exceptions;

public class BoardAlreadyInitializedException : Exception
{
    public BoardAlreadyInitializedException(string message) : base(message)
    {
    }
    
    public static void Throw() => throw new BoardAlreadyInitializedException("Board must be initialized only once.");
}