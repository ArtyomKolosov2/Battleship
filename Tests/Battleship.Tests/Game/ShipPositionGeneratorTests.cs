using System.Reflection;
using Battleship.Core.Exceptions;
using Battleship.Core.Game;
using Battleship.Core.Game.Services;
using Battleship.Core.Models;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Panel;
using Battleship.Tests.FakeObjects;
using FluentAssertions;

namespace Battleship.Tests.Game;

public class ShipPositionGeneratorTests
{
    private const string BoardInternalPanelsFieldName = "_panels";

    [Fact]
    public void AddShipsToBoard_MoreShipsOverMaxBoardCapacity_ExceptionThrown()
    {
        // Arrange
        var board = new Board();
        board.InitBoard();
        
        var shipGenerator = new ShipPositionGenerator();

        var ships = Enumerable.Repeat(new FakeOnePanelShip(), Board.MaxRows * Board.MaxColumns + 1);
          
        // Act
        var action = () => shipGenerator.AddShipsToBoard(board, ships);
        
        // Assert
        action.Should().Throw<ShipCantBePlacedException>().WithMessage($"Ship {nameof(FakeOnePanelShip)} can't be placed on board because there isn't enough space");
    }
    
    [Fact]
    public void AddShipsToBoard_SeveralBoardsAndStandardShipsUsed_ShipsPlacedAtRandomLocationEverytime()
    {
        // Arrange
        const int testIterations = 10;
        var listOfGeneratedCoordinates = new List<List<Coordinates>>();
        
        // Act & Assert
        for (var i = 0; i < testIterations; i++)
        {
            listOfGeneratedCoordinates.Add(new List<Coordinates>());
            
            var board = new Board();
            board.InitBoard();
        
            var shipGenerator = new ShipPositionGenerator();
            
            var ships = new Ship[]
            {
                new Destroyer(),
                new Destroyer(),
                new Core.Models.Battleship()
            };
            
            var action = () => shipGenerator.AddShipsToBoard(board, ships);
            
            action.Should().NotThrow();
        
            var boardPanels = board.GetType().GetField(BoardInternalPanelsFieldName, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(board) as Panel[,];
            boardPanels.Should().NotBeNull("If not found then private field is renamed or it's type was changed");

            foreach (var ship in ships)
            {
                var panelsWithShip = boardPanels!.Cast<Panel>().Where(x => ReferenceEquals(x.Ship, ship)).ToList();
                panelsWithShip.Should().HaveCount(ship.Size);
                
                var panelsWithShipCoordinates = panelsWithShip.Select(x => x.Coordinates).ToList();
                panelsWithShipCoordinates.Should().OnlyHaveUniqueItems("We should not have ship intersections");
                listOfGeneratedCoordinates[i].AddRange(panelsWithShipCoordinates);
            }
        }

        var coordinatesDifferences = listOfGeneratedCoordinates.Aggregate((x, y) => x.Except(y).ToList());
        coordinatesDifferences.Should().NotBeEmpty();
    }
}