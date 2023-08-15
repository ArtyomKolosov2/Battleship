using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.Game.Services;

public interface IShipFactory
{
    IEnumerable<Ship> BuildShips();
}