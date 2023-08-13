using Battleship.Core.ValueObjects.Common;

namespace Battleship.Core.ValueObjects;

public record Coordinates(NonNegativeNumber Row, NonNegativeNumber Column);