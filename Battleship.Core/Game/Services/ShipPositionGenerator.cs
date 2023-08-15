using Battleship.Core.Exceptions;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Panel;
using Battleship.Core.ValueObjects.ShipDirection;
using Battleship.Shared;

namespace Battleship.Core.Game.Services;

public class ShipPositionGenerator : IShipPositionGenerator 
{
    public void AddShipsToBoard(Board.Board board, IEnumerable<Ship> ships)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        foreach (var ship in ships)
            PlaceShipAtRandomSpot(board, randomizer, ship);
    }

    private static void PlaceShipAtRandomSpot(Board.Board board, Random randomizer, Ship ship)
    {
        const int maxPlacementsAttempts = 100; 
        
        var shipPlaced = false;
        var shipPlacementsAttempts = 0;
        while (!shipPlaced)
        {
            if (shipPlacementsAttempts > maxPlacementsAttempts)
                ShipCantBePlacedException.Throw(ship);
            
            var startCoordinates = GetRandomStartCoordinates(randomizer);
            var direction = GetRandomShipDirection(randomizer);

            var getPanelsOption = TryGetPanelsToPlaceShip(board, direction, startCoordinates, ship);
            
            shipPlacementsAttempts++;
            if (!getPanelsOption.IsSome(out var panelsToPlaceShip))
                continue;

            panelsToPlaceShip.ForEach(x => x.AddShip(ship));
            shipPlaced = true;
        }
    }

    private static Coordinates GetRandomStartCoordinates(Random randomizer) => new(randomizer.Next(0, Board.Board.MaxRows), randomizer.Next(0, Board.Board.MaxColumns));

    private static Option<List<Panel>> TryGetPanelsToPlaceShip(Board.Board board, ShipDirectionValue direction, Coordinates currentShipCoordinates, Ship ship)
    {
        var panelsToPlaceShip = new List<Panel>();
        for (var i = 0; i < ship.Size; i++)
        {
            var result = GetNextCoordinates(direction, currentShipCoordinates);

            if (!result.IsSome(out var coordinates))
                return Option<List<Panel>>.None;
            
            var panelResult = board.GetPanelAtCoords(currentShipCoordinates);
            
            if (panelResult.IsSuccess && !panelResult.Data!.IsOccupiedByShip)
                panelsToPlaceShip.Add(panelResult.Data);
            else
                return Option<List<Panel>>.None;

            currentShipCoordinates = coordinates;
        }

        return panelsToPlaceShip.Count == ship.Size 
            ? Option<List<Panel>>.Some(panelsToPlaceShip)
            : Option<List<Panel>>.None;
    }

    private static Option<Coordinates> GetNextCoordinates(ShipDirectionValue shipDirection, Coordinates coordinates)
    {
        (int row, int column) nextCoordinates = shipDirection switch
        {
            ShipDirectionValue.Left => (coordinates.Row, coordinates.Column - 1),
            ShipDirectionValue.Right => (coordinates.Row, coordinates.Column + 1),
            ShipDirectionValue.Top => (coordinates.Row + 1, coordinates.Column),
            ShipDirectionValue.Bottom => (coordinates.Row - 1, coordinates.Column),
            _ => throw new ArgumentOutOfRangeException(nameof(shipDirection), "Unexpected value of ship direction.")
        };

        return nextCoordinates switch
        {
            { column: < 0 or >= Board.Board.MaxRows } or { row: < 0 or >= Board.Board.MaxColumns } => Option<Coordinates>.None,
            _ => Option<Coordinates>.Some(new Coordinates(nextCoordinates.row, nextCoordinates.column))
        };
    }

    private static ShipDirectionValue GetRandomShipDirection(Random randomizer) 
        => (ShipDirectionValue)randomizer.Next((int)ShipDirectionValue.Left, (int)ShipDirectionValue.Bottom + 1);
}

