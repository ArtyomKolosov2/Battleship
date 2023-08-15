using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.Exceptions;

public class ShipCantBePlacedException : Exception
{
    public Ship Ship { get; }

    private ShipCantBePlacedException(string message, Ship ship) : base(message)
    {
        Ship = ship;
    }

    public static void Throw(Ship ship) => throw new ShipCantBePlacedException($"Ship {ship.GetType().Name} can't be placed on board because there isn't enough space", ship);
}