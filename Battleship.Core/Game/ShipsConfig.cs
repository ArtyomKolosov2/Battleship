using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.Game;

public record ShipsConfig
{
    public NonNegativeNumber BattleshipsCount { get; init; }
    
    public NonNegativeNumber DestroyersCount { get; init;  }

    public static ShipsConfig Default => new()
    {
        BattleshipsCount = 1,
        DestroyersCount = 2
    };
}