using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Shot;

namespace Battleship.Core.ValueObjects.Panel;

internal class Panel
{
    internal Ship? Ship { get; private set; }
    
    public PanelStatusValue StatusValue { get; private set; }

    public Coordinates Coordinates { get; }
    
    public bool IsOccupiedByShip => Ship is not null;
    
    public Panel(NonNegativeNumber row, NonNegativeNumber column)
    {
        Coordinates = new Coordinates(row, column);
    }

    internal void AddShip(Ship ship)
    {
        if (IsOccupiedByShip)
            return;

        Ship = ship;
    }

    internal ShotResult RegisterShot()
    {
        if (IsOccupiedByShip)
        {
            StatusValue = PanelStatusValue.Hit;
            Ship!.AddHit();
        }
        else
            StatusValue = PanelStatusValue.Miss;

        return StatusValue switch
        {
            PanelStatusValue.Hit when Ship!.IsDestroyed => ShotResult.CreateSunk(Coordinates, Ship),
            PanelStatusValue.Hit => ShotResult.CreateHit(Coordinates, Ship),
            PanelStatusValue.Miss => ShotResult.CreateMiss(Coordinates),
            _ => throw new ArgumentOutOfRangeException(nameof(StatusValue), $"Unexpected panel state.")
        };
    }
}