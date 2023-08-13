using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Output;

public interface IBattleshipOutput
{
    // Introduce interface of board
    void OutputCurrentStateOfBoard(IEnumerable<Panel> board);

    void OutputGameMessage(NotEmptyString message);
}