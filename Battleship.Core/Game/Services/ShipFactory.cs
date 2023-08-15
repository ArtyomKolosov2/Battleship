using Battleship.Core.Models;
using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.Game.Services;

public class ShipFactory : IShipFactory
{
    private readonly ShipsConfig _shipsConfig;

    public ShipFactory(ShipsConfig shipsConfig)
    {
        _shipsConfig = shipsConfig;
    }
    
    public IEnumerable<Ship> BuildShips()
    {
        var battleships = Enumerable.Range(0, _shipsConfig.BattleshipsCount).Select(_ => new Models.Battleship());
        var destroyers = Enumerable.Range(0, _shipsConfig.DestroyersCount).Select(_ => new Destroyer());

        return battleships.Concat<Ship>(destroyers);
    }
}