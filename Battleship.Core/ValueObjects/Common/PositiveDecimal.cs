using System.Globalization;
using Battleship.Core.ValueObjects.Exceptions;

namespace Battleship.Core.ValueObjects.Common
{
    public record struct PositiveDecimal : IValueObject<decimal>
    {
        public PositiveDecimal(decimal value)
        {
            Value = value <= 0 ? throw new ValueObjectException<PositiveNumber>("Given amount can't be negative or equal to zero") : value;
        }

        public decimal Value { get; private set; }

        public static implicit operator decimal(PositiveDecimal value) => value.Value;

        public static implicit operator PositiveDecimal(decimal value) => new(value);

        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
