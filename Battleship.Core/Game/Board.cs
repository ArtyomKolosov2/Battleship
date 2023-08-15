using Battleship.Core.Exceptions;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Error;
using Battleship.Core.ValueObjects.Panel;
using Battleship.Core.ValueObjects.ShipDirection;
using Battleship.Shared;

namespace Battleship.Core.Game;

public class Board : IBoard
{
    public const int MaxRows = 10;
    public const int MaxColumns = 10;

    private readonly Panel[,] _panels = new Panel[MaxRows, MaxColumns];
    
    internal void InitBoard()
    {
        if (_panels.Length > 0)
            return;
            
        for (var row = 0; row < MaxRows; row++)
            for (var column = 0; column < MaxColumns; column++)
                _panels[row, column] = new Panel(row, column);
    }

    internal void AddShipsToBoard(IReadOnlyCollection<Ship> ships)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        foreach (var ship in ships)
            PlaceShipAtRandomSpot(randomizer, ship);
    }

    private void PlaceShipAtRandomSpot(Random randomizer, Ship ship)
    {
        const int maxPlacementsAttempts = 100; 
        
        var shipPlaced = false;
        var shipPlacementsAttempts = 0;
        while (!shipPlaced)
        {
            if (shipPlacementsAttempts > maxPlacementsAttempts)
                ShipCantBePlacedException.Throw(ship);
            
            var startCoordinates = GetStartCoordinates(randomizer);
            var direction = GetShipDirection(randomizer);

            var getPanelsOption = TryGetPanelsToPlaceShip(ship, direction, startCoordinates);
            
            shipPlacementsAttempts++;
            if (!getPanelsOption.IsSome(out var panelsToPlaceShip))
                continue;

            panelsToPlaceShip.ForEach(x => x.AddShip(ship));
            shipPlaced = true;
        }
    }

    private static Coordinates GetStartCoordinates(Random randomizer) => new(randomizer.Next(0, MaxRows), randomizer.Next(0, MaxColumns));

    private Option<List<Panel>> TryGetPanelsToPlaceShip(Ship ship, ShipDirectionValue direction, Coordinates currentShipCoordinates)
    {
        var panelsToPlaceShip = new List<Panel>();
        for (var i = 0; i < ship.Size; i++)
        {
            var result = GetNextCoordinates(direction, currentShipCoordinates);

            if (!result.IsSome(out var coordinates))
                return Option<List<Panel>>.None;
            
            var panelResult = GetPanelAtCoords(currentShipCoordinates);
            
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
            { column: < 0 or >= MaxColumns } or { row: < 0 or >= MaxRows } => Option<Coordinates>.None,
            _ => Option<Coordinates>.Some(new Coordinates(nextCoordinates.row, nextCoordinates.column))
        };
    }

    private static ShipDirectionValue GetShipDirection(Random randomizer) 
        => (ShipDirectionValue)randomizer.Next((int)ShipDirectionValue.Left, (int)ShipDirectionValue.Bottom + 1);

    internal Result<Panel, GameError> GetPanelAtCoords(Coordinates coordinates)
    {
        if (_panels.Length == 0)
            throw new PanelNotInitializedException("Panels must be initialized on the board.");

        return _panels.GetValue(coordinates.Row, coordinates.Column) is Panel panel
            ? Result<Panel, GameError>.Success(panel)
            : Result<Panel, GameError>.Failure(GameError.WithMessage("Provided coordinates were invalid."));
    }

    public IEnumerable<IEnumerable<Panel>> GetPanels()
    {
        var rowCount = _panels.GetLength(0);
        var colCount = _panels.GetLength(1);

        for (var i = 0; i < rowCount; i++)
            yield return Enumerable.Range(0, colCount).Select(j => _panels[i, j]).ToList();
    }
}