using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Shot;

namespace Battleship.Core.ValueObjects.Panel;

public class Panel
{
    private Ship? _ship;
    
    public PanelStatus Status { get; private set; }

    public Coordinates Coordinates { get; }
    
    public bool IsOccupiedByShip => _ship is not null;
    
    public Panel(NonNegativeNumber row, NonNegativeNumber column)
    {
        Coordinates = new Coordinates(row, column);
    }

    internal void AddShip(Ship ship)
    {
        if (IsOccupiedByShip)
            return;

        _ship = ship;
    }

    internal ShotResult RegisterShot()
    {
        if (IsOccupiedByShip)
        {
            Status = PanelStatus.Hit;
            _ship!.AddHit();
        }
        else
            Status = PanelStatus.Miss;

        return Status switch
        {
            PanelStatus.Hit when _ship!.IsDestroyed => ShotResult.CreateSunk(Coordinates, _ship),
            PanelStatus.Hit => ShotResult.CreateHit(Coordinates, _ship),
            PanelStatus.Miss => ShotResult.CreateMiss(Coordinates),
            _ => throw new ArgumentOutOfRangeException(nameof(Status), "Unexpected panel state!")
        };
    }
}