using Battleship.Core.Game;
using Battleship.Core.ValueObjects;
using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Output;

public interface IBattleshipOutput
{
    void OutputCurrentStateOfBoard(IBoard board);

    void OutputGameMessage(NotEmptyString message);
}