using System.Collections;
using Battleship.Core.Exceptions;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Error;
using Battleship.Core.ValueObjects.Panel;
using Battleship.Shared;

namespace Battleship.Core.Game;

public class Board : IBoard
{
    public const int MaxRows = 10;
    public const int MaxColumns = 10;

    private readonly List<Panel> _panels = new();
    
    internal void InitBoard()
    {
        if (_panels.Any())
            return;
            
        for (var row = 0; row < MaxRows; row++)
            for (var column = 0; column < MaxColumns; column++)
                _panels.Add(new Panel(row, column));
    }

    internal void AddShipsToBoard(IReadOnlyCollection<Ship> ships)
    {
        var randomizer = new Random(Guid.NewGuid().GetHashCode());
        var firstShip = ships.First();
        var counter = 0;
        
        foreach (var panel in this)
        {
            panel.AddShip(firstShip);
            counter++;
            if (counter == firstShip.Size)
                break;
        }
    }

    internal Result<Panel, GameError> GetPanelAtCoords(Coordinates coordinates)
    {
        if (!_panels.Any())
            throw new PanelNotInitializedException("Panels must be initialized on the board.");
        
        var panel = _panels.SingleOrDefault(x => x.Coordinates == coordinates);

        return panel is not null
            ? Result<Panel, GameError>.Success(panel)
            : Result<Panel, GameError>.Failure(GameError.WithMessage("Provided coordinates were invalid."));
    }

    public IEnumerator<Panel> GetEnumerator() => _panels.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}