using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Game;

public interface IBoardViewModel
{
    IEnumerable<IEnumerable<PanelViewModel>> GetBoardPanelsViewModels();
}