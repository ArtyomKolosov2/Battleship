using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.Models.Abstractions;

public abstract class Ship
{
    private int _hitCount;
    
    public NotEmptyString Name { get; }
    
    public PositiveNumber Size { get; init; }
    
    protected Ship(PositiveNumber size, NotEmptyString name)
    {
        Size = size;
        Name = name;
    }

    public void AddHit()
    {
        if (IsDestroyed)
            return;
        
        _hitCount++;
    }

    public bool IsDestroyed => _hitCount == Size.Value;
}