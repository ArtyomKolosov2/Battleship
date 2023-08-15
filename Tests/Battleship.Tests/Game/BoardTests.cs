using Battleship.Core.Exceptions;
using Battleship.Core.Game;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Panel;
using Battleship.Core.ValueObjects.Shot;
using Battleship.Tests.FakeObjects;
using FluentAssertions;

namespace Battleship.Tests.Game;

public class BoardTests
{
    [Fact]
    public void InitBoard_BoardNotInitializedAndMethodCalled_PanelsFilled()
    {
        // Arrange
        var board = new Board();

        // Act
        var action = () => board.InitBoard();
        var getBoardPanelsViewModelsAction = () => board.GetBoardPanelsViewModels().ToArray();

        // Assert
        action.Should().NotThrow();
        getBoardPanelsViewModelsAction.Should().NotThrow<BoardNotInitializedException>();
    }

    [Fact]
    public void InitBoard_BoardAlreadyInitialized_ExceptionThrown()
    {
        // Arrange
        var board = new Board();

        // Act
        board.InitBoard();
        var action = () => board.InitBoard();

        // Assert
        action.Should().Throw<BoardAlreadyInitializedException>().WithMessage("Board must be initialized only once.");
    }

    [Theory]
    [InlineData(ShotResultValue.Hit, 0, 0)]
    [InlineData(ShotResultValue.Sunk, 1, 0)]
    [InlineData(ShotResultValue.Miss, 2, 2)]
    public void RegisterShot_ProvidedCoordinatesAreValid_ShotRegisteredWithExpectedStatus(ShotResultValue expectedShotValue, int row, int column)
    {
        // Arrange
        var fakePositionGenerator = new FakeShipPositionGenerator();
        var board = new Board();
        board.InitBoard();
        fakePositionGenerator.AddShipsToBoard(board, new Ship[] { new FakeTwoPanelShip(), new FakeOnePanelShip() });

        var targetCoordinates = new Coordinates(row, column);

        // Act
        var registerShotInResult = board.RegisterShot(targetCoordinates);

        // Assert
        registerShotInResult.IsSuccess.Should().BeTrue();

        var shotResult = registerShotInResult.Data;
        shotResult!.Coordinates.Should().Be(targetCoordinates);
        shotResult.ShotResultValue.Should().Be(expectedShotValue);

        if (expectedShotValue is ShotResultValue.Hit)
            shotResult.Ship.Should().NotBeNull();
    }

    [Fact]
    public void RegisterShot_HitsRegisteredTwice_FailedResultReturned()
    {
        // Arrange
        var board = new Board();
        board.InitBoard();
        var targetCoordinates = new Coordinates(1, 1);

        // Act & Assert
        var firstRegisterShotInResult = board.RegisterShot(targetCoordinates);
        firstRegisterShotInResult.IsSuccess.Should().BeTrue();

        var secondRegisterShotInResult = board.RegisterShot(targetCoordinates);
        secondRegisterShotInResult.IsFailure.Should().BeTrue();
        secondRegisterShotInResult.Error!.Message.Should().Be("Cannot register hit on coordinates. It was already hit.");
    }

    [Fact]
    public void GetPanelAtCoords_BoardNotInitialized_ExceptionThrown()
    {
        // Arrange
        var board = new Board();
        var targetCoordinates = new Coordinates(1, 1);

        // Act & Assert
        var action = () => board.GetPanelAtCoords(targetCoordinates);
        action.Should().Throw<BoardNotInitializedException>().WithMessage("Panels must be initialized on the board.");
    }

    [Fact]
    public void GetPanelAtCoords_BoardInitialized_PanelWithStandardStateReturned()
    {
        // Arrange
        var board = new Board();
        board.InitBoard();
        var targetCoordinates = new Coordinates(1, 1);

        // Act & Assert
        var getResult = board.GetPanelAtCoords(targetCoordinates);
        getResult.IsSuccess.Should().BeTrue();

        getResult.Data!.Coordinates.Should().Be(targetCoordinates);
        getResult.Data.StatusValue.Should().Be(PanelStatusValue.Empty);
        getResult.Data.IsOccupiedByShip.Should().BeFalse();
    }

    [Fact]
    public void GetPanelAtCoords_BoardInitializedAndOneShipIsPlaced_PanelWithStateWithShipReturned()
    {
        // Arrange
        var fakePositionGenerator = new FakeShipPositionGenerator();
        var board = new Board();
        board.InitBoard();
        fakePositionGenerator.AddShipsToBoard(board, new Ship[] { new FakeOnePanelShip() });

        var targetCoordinates = new Coordinates(0, 0);

        // Act & Assert
        var getResult = board.GetPanelAtCoords(targetCoordinates);
        getResult.IsSuccess.Should().BeTrue();

        getResult.Data!.Coordinates.Should().Be(targetCoordinates);
        getResult.Data.StatusValue.Should().Be(PanelStatusValue.Empty);
        getResult.Data.IsOccupiedByShip.Should().BeTrue();
    }
    
    [Fact]
    public void GetBoardPanelsViewModels_BoardNotInitialized_ExceptionThrown()
    {
        // Arrange
        var board = new Board();

        // Act & Assert
        var action = () => board.GetBoardPanelsViewModels().ToArray();
        action.Should().Throw<BoardNotInitializedException>().WithMessage("Panels must be initialized on the board.");
    }
    
    [Fact]
    public void GetBoardPanelsViewModels_BoardInitialized_ViewModelsReturned()
    {
        // Arrange
        var board = new Board();
        board.InitBoard();
        
        // Act
        var viewModels = board.GetBoardPanelsViewModels().ToList();
        
        // Assert
        viewModels.Should().NotBeEmpty();
        viewModels.Should().AllSatisfy(x => x.Should().NotBeEmpty().And.HaveCount(Board.MaxColumns))
            .And.HaveCount(Board.MaxRows);
        viewModels.SelectMany(x => x).Should().OnlyContain(x => x.StatusValue == PanelStatusValue.Empty);
    }
}