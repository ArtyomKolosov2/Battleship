using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.ValueObjects;

public record Coordinates
{
    public NonNegativeNumber Row { get; }
    public NonNegativeNumber Column { get; }

    public Coordinates(NonNegativeNumber row, NonNegativeNumber column)
    {
        Row = row;
        Column = column;
    }
}