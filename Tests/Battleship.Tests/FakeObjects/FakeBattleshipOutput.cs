using Battleship.Core.Game;
using Battleship.Core.Output;
using Battleship.Core.ValueObjects.Common;

namespace Battleship.Tests.FakeObjects;

public class FakeBattleshipOutput : IBattleshipOutput
{
    public Queue<string> MessageQueue { get; init; } = new();
    
    public void OutputCurrentStateOfBoard(IBoardViewModel boardViewModel)
    {
    }

    public void OutputGameMessage(NotEmptyString message)
    {
        MessageQueue.Enqueue(message.ToString());
    }
}