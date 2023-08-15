using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.ValueObjects.Shot;

public record ShotResult
{
    public ShotResultValue ShotResultValue { get; }
    public Ship? Ship { get; }
    public Coordinates Coordinate { get; }

    private ShotResult(Coordinates coordinates, ShotResultValue shotResultValue, Ship? ship = null)
    {
        Coordinate = coordinates;
        ShotResultValue = shotResultValue;
        Ship = ship;
    }

    public static ShotResult CreateMiss(Coordinates coordinates) => new(coordinates, ShotResultValue.Miss);
    
    public static ShotResult CreateSunk(Coordinates coordinates, Ship ship) => new(coordinates, ShotResultValue.Sunk, ship);
    
    public static ShotResult CreateHit(Coordinates coordinates, Ship ship) => new(coordinates, ShotResultValue.Hit, ship);
}