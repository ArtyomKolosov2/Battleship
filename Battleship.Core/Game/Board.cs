using System.Collections;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Game;

public class Board : IEnumerable<Panel>
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

    internal Panel? GetPanelAtCoords(Coordinates coordinates)
    {
        if (!_panels.Any())
            return null;
        
        return _panels.SingleOrDefault(x => x.Coordinates == coordinates);
    }

    public IEnumerator<Panel> GetEnumerator() => _panels.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}