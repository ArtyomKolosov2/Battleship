using Battleship.Core.ValueObjects.Panel;

namespace Battleship.Core.Game;

public interface IBoard
{
    IEnumerable<IEnumerable<Panel>> GetPanels();
}