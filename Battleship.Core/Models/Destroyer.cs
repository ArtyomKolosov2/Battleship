using Battleship.Core.Models.Abstractions;

namespace Battleship.Core.Models;

public class Destroyer : Ship
{
    private const int DestroyerSize = 4;
    
    public Destroyer() : base(DestroyerSize, "Destroyer")
    {
    }
}