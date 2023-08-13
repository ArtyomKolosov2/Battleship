using Battleship.Core.ValueObjects.Exceptions;

namespace Battleship.Core.ValueObjects.ShipDirection;

public record ShipDirection : IValueObject<ShipDirectionUnit>
{
    public ShipDirectionUnit Value { get; }
    
    public ShipDirection(ShipDirectionUnit shipDirectionUnit)
    {
        Value = shipDirectionUnit switch
        {
            >= ShipDirectionUnit.Left and <= ShipDirectionUnit.Bottom => shipDirectionUnit,
            _ => throw new ValueObjectException<ShipDirection>("ShipDirectionUnit can't be used."),
        };
    }
}