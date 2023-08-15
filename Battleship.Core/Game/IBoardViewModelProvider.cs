using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Game;

public interface IBoardViewModelProvider
{
    IEnumerable<IEnumerable<PanelViewModel>> GetBoardPanelsViewModels();
}