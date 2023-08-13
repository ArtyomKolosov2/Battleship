using Battleship.Core.ValueObjects;

namespace Battleship.Core.Input;

public interface IBattleshipInput
{
    Coordinates GetCoordinatesFromUser();
}