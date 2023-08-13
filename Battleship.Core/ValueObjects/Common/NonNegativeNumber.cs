using Battleship.Core.ValueObjects.Exceptions;

namespace Battleship.Core.ValueObjects.Common
{
    public record struct NonNegativeNumber : IValueObject<int>
    {
        public NonNegativeNumber(int value)
        {
            Value = value < 0 ? throw new ValueObjectException<NonNegativeNumber>("Given number can't be negative") : value;
        }

        public int Value { get; private set; }

        public static implicit operator int(NonNegativeNumber value) => value.Value;

        public static implicit operator NonNegativeNumber(int value) => new(value);
    }
}
