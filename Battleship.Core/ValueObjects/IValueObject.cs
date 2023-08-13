namespace Battleship.Core.ValueObjects;

public interface IValueObject<out T>
{
    T Value { get; }
}