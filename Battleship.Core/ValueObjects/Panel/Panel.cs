using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Shot;

namespace Battleship.Core.ValueObjects.Panel;

internal class Panel
{
    private Ship? _ship;
    
    public PanelStatusValue StatusValue { get; private set; }

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
            StatusValue = PanelStatusValue.Hit;
            _ship!.AddHit();
        }
        else
            StatusValue = PanelStatusValue.Miss;

        return StatusValue switch
        {
            PanelStatusValue.Hit when _ship!.IsDestroyed => ShotResult.CreateSunk(Coordinates, _ship),
            PanelStatusValue.Hit => ShotResult.CreateHit(Coordinates, _ship),
            PanelStatusValue.Miss => ShotResult.CreateMiss(Coordinates),
            _ => throw new ArgumentOutOfRangeException(nameof(StatusValue), $"Unexpected panel state.")
        };
    }
}