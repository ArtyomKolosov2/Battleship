using Battleship.Core.Game;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;
using Battleship.Tests.FakeObjects;
using FluentAssertions;

namespace Battleship.Tests.Game;

public class GameEngineTests
{
    [Fact]
    public void AssertGeneralGameProcess_OnePanelShipPlaced_DestroyedAndGameEnded()
    {
        var fakeInput = new FakeBattleshipInput();
        fakeInput.CoordinatesQueue.Enqueue(new Coordinates(0, 0));
        
        var fakeOutput = new FakeBattleshipOutput();
        var fakeShipPositionGenerator = new FakeShipPositionGenerator();
        var ship = new FakeOnePanelShip();
        var fakeShipFactory = new FakeShipFactory(new Ship[] { ship });
        
        var gameEngine = new GameEngine(fakeInput, fakeOutput, fakeShipPositionGenerator, fakeShipFactory);
        var action = () => gameEngine.StartGame();
        action.Should().NotThrow();

        var message1 = fakeOutput.MessageQueue.Dequeue();
        message1.Should().Be($"Sunk, {ship.Name}!");

        var message2 = fakeOutput.MessageQueue.Dequeue();
        message2.Should().Be("All ships are destroyed! You won!");
    }
    
    [Fact]
    public void AssertGeneralGameProcess_TwoPanelShipPlaced_MissAndHitAndSunkAndGameEnded()
    {
        var fakeInput = new FakeBattleshipInput();
        fakeInput.CoordinatesQueue.Enqueue(new Coordinates(2, 2));
        fakeInput.CoordinatesQueue.Enqueue(new Coordinates(0, 0));
        fakeInput.CoordinatesQueue.Enqueue(new Coordinates(0, 1));
        
        var fakeOutput = new FakeBattleshipOutput();
        var fakeShipPositionGenerator = new FakeShipPositionGenerator();
        var ship = new FakeTwoPanelShip();
        var fakeShipFactory = new FakeShipFactory(new Ship[] { ship });
        
        var gameEngine = new GameEngine(fakeInput, fakeOutput, fakeShipPositionGenerator, fakeShipFactory);
        var action = () => gameEngine.StartGame();
        action.Should().NotThrow();

        var message1 = fakeOutput.MessageQueue.Dequeue();
        message1.Should().Be("Miss!");
        
        var message2 = fakeOutput.MessageQueue.Dequeue();
        message2.Should().Be($"Hit, {ship.Name}!");
        
        var message3 = fakeOutput.MessageQueue.Dequeue();
        message3.Should().Be($"Sunk, {ship.Name}!");

        var message4 = fakeOutput.MessageQueue.Dequeue();
        message4.Should().Be("All ships are destroyed! You won!");
    }
}