using Battleship.Core.Game;
using Battleship.Core.Game.Services;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;

namespace Battleship.Tests.FakeObjects;


internal class FakeShipPositionGenerator : IShipPositionGenerator
{
    public void AddShipsToBoard(Board board, IEnumerable<Ship> ships)
    {
        var row = 0;
        foreach (var ship in ships)
        {
            var coordinatesForFirstRow = Enumerable.Range(0, ship.Size)
                .Select(column => new Coordinates(row, column));

            foreach (var coordinates in coordinatesForFirstRow)
                board.GetPanelAtCoords(coordinates).Data!.AddShip(ship);

            row++;
        }
    }
}