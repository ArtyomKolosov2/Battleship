using Battleship.Core.Game.Services;
using Battleship.Core.Models.Abstractions;

namespace Battleship.Tests.FakeObjects;

public class FakeShipFactory : IShipFactory
{
    private readonly IEnumerable<Ship> _ships;

    public FakeShipFactory(IEnumerable<Ship> ships)
    {
        _ships = ships;
    }
    
    public IEnumerable<Ship> BuildShips() => _ships;
}