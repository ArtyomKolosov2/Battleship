using System.Globalization;
using Battleship.Core.ValueObjects.Exceptions;

namespace Battleship.Core.ValueObjects.Common
{
    public record struct PositiveDouble : IValueObject<double>
    {
        public PositiveDouble(double value)
        {
            Value = value <= 0 ? throw new ValueObjectException<PositiveDouble>("Given amount can't be negative or equal to zero") : value;
        }

        public double Value { get; private set; }

        public static implicit operator double(PositiveDouble value) => value.Value;

        public static implicit operator PositiveDouble(double value) => new(value);

        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
