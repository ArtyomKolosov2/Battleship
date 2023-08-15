using Battleship.Core.Game;
using Battleship.Core.Game.Services;
using Battleship.Core.Models.Abstractions;
using Battleship.Core.ValueObjects;

namespace Battleship.Tests.FakeObjects;


internal class FakeTwoPanelShip : Ship
{
    public FakeTwoPanelShip() : base(2, "Fake2")
    {
    }
}

internal class FakeOnePanelShip : Ship
{
    public FakeOnePanelShip() : base(1, "Fake1")
    {
    }
}
