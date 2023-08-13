using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.ValueObjects.Shot;

public record ShotResult
{
    public ShotResultEnum ShotResultEnum { get; }
    public Ship? Ship { get; }
    public Coordinates Coordinate { get; }

    private ShotResult(Coordinates coordinates, ShotResultEnum shotResultEnum, Ship? ship = null)
    {
        Coordinate = coordinates;
        ShotResultEnum = shotResultEnum;
        Ship = ship;
    }

    public static ShotResult CreateMiss(Coordinates coordinates) => new(coordinates, ShotResultEnum.Miss);
    
    public static ShotResult CreateSunk(Coordinates coordinates, Ship ship) => new(coordinates, ShotResultEnum.Sunk, ship);
    
    public static ShotResult CreateHit(Coordinates coordinates, Ship ship) => new(coordinates, ShotResultEnum.Hit, ship);
}