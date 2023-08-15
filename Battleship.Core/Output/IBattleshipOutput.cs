using Battleship.Core.Game;
using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.Output;

public interface IBattleshipOutput
{
    void OutputCurrentStateOfBoard(IBoardViewModelProvider boardViewModelProvider);

    void OutputGameMessage(NotEmptyString message);
}