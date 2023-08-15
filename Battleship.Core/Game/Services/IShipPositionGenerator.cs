using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.Game.Services;

public interface IShipPositionGenerator
{
    void AddShipsToBoard(Board board, IEnumerable<Ship> ships);
}