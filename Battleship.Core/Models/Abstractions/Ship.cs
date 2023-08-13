using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.Models.Abstractions;

public abstract class Ship
{
    private int _hitCount;
    
    public PositiveNumber Size { get; init; }
    
    protected Ship(PositiveNumber size)
    {
        Size = size;
    }

    public void AddHit()
    {
        if (IsDestroyed)
            return;
        
        _hitCount++;
    }

    public bool IsDestroyed => _hitCount == Size.Value;
}