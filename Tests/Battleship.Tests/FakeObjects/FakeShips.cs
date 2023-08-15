using Battleship.Core.Models.Abstractions;

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
