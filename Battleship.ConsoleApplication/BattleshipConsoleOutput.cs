using Battleship.Core.Output;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Panel;

namespace Battleship.ConsoleApplication;

public class BattleshipConsoleOutput : IBattleshipOutput
{
    public void OutputCurrentStateOfBoard(IEnumerable<Panel> board)
    {
        
    }

    public void OutputGameMessage(NotEmptyString message)
    {
        Console.WriteLine(message.ToString());
    }
}