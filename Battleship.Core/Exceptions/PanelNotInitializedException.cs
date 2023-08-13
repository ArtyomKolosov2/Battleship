namespace Battleship.Core.Exceptions;

public class PanelNotInitializedException : Exception
{
    public PanelNotInitializedException(string message) : base(message)
    {
    }
}