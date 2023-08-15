using Battleship.Core.Game;
using Battleship.Core.Game.Board;
using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.Output;

public interface IBattleshipOutput
{
    void OutputCurrentStateOfBoard(IBoardViewModel boardViewModel);

    void OutputGameMessage(NotEmptyString message);
}