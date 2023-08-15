using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.Models;

public class Battleship : Ship
{
    private const int BattleshipSize = 5;
    
    public Battleship() : base(BattleshipSize, "Battleship")
    {
    }
}