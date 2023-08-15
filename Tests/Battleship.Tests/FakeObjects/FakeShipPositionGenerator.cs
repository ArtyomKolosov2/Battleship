﻿using Battleship.Core.Game;
using Battleship.Core.Game.Services;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;

namespace Battleship.Tests.FakeObjects;


internal class FakeShipPositionGenerator : IShipPositionGenerator
{
    private readonly IEnumerable<Ship>? _ships;

    public FakeShipPositionGenerator(IEnumerable<Ship>? ships = null)
    {
        _ships = ships;
    }
    
    public void AddShipsToBoard(Board board, IEnumerable<Ship> ships)
    {
        var row = 0;
        foreach (var ship in _ships ?? ships)
        {
            var coordinatesForFirstRow = Enumerable.Range(0, ship.Size)
                .Select(column => new Coordinates(row, column));

            foreach (var coordinates in coordinatesForFirstRow)
                board.GetPanelAtCoordinates(coordinates).Data!.AddShip(ship);

            row++;
        }
    }
}