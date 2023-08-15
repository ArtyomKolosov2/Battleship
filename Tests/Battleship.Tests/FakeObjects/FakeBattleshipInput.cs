using Battleship.Core.Input;
using Battleship.Core.ValueObjects;

namespace Battleship.Tests.FakeObjects;

public class FakeBattleshipInput : IBattleshipInput
{
    public Queue<Coordinates> CoordinatesQueue { get; init; } = new();

    public Coordinates GetCoordinatesFromUser() => CoordinatesQueue.Dequeue();
}