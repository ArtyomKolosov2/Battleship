using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Game.Board;

public interface IBoardViewModel
{
    IEnumerable<IEnumerable<PanelViewModel>> GetBoardPanelsViewModels();
}