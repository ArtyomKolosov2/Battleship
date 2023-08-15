using Battleship.Core.Game;
using Battleship.Core.Game.Services;
using Battleship.Core.Models;
using FluentAssertions;

namespace Battleship.Tests.Game;

public class ShipFactoryTests
{
    [Theory]
    [InlineData(2, 2)]
    [InlineData(0, 0)]
    public void Generate(int battleshipAmount, int destroyerAmount)
    {
        // Arrange
        var config = new ShipsConfig
        {
            BattleshipsCount = battleshipAmount,
            DestroyersCount = destroyerAmount
        };

        var factory = new ShipFactory(config);
        
        // Act
        var ships = factory.BuildShips().ToList();

        ships.OfType<Core.Models.Battleship>().Should().HaveCount(battleshipAmount);
        ships.OfType<Destroyer>().Should().HaveCount(destroyerAmount);
    } 
}