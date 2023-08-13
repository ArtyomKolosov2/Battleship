using Battleship.Core.ValueObjects.Exceptions;

namespace Battleship.Core.ValueObjects.Common
{
    public record struct PositiveNumber : IValueObject<int>
    {
        public PositiveNumber(int value)
        {
            Value = value <= 0 ? throw new ValueObjectException<PositiveNumber>("Given number can't be negative or equal to zero") : value;
        }

        public int Value { get; private set; }

        public static implicit operator int(PositiveNumber value) => value.Value;

        public static implicit operator PositiveNumber(int value) => new(value);

        public override string ToString() => Value.ToString();
    }
}
