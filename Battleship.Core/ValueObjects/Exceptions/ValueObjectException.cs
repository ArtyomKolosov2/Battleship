namespace Battleship.Core.ValueObjects.Exceptions;

internal class ValueObjectException<T> : Exception
{
    public readonly string TypeName = typeof(T).Name;

    public ValueObjectException(string message) : base(message)
    {
            
    }
}