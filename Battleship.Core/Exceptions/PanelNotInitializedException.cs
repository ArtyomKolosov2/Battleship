namespace Battleship.Core.Exceptions;

public class PanelNotInitializedException : Exception
{
    public PanelNotInitializedException(string message) : base(message)
    {
    }
    
    public static void Throw() => throw new PanelNotInitializedException("Panels must be initialized on the board.");
}