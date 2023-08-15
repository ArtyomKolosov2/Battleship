using Battleship.Core.ValueObjects.Exceptions;

namespace Battleship.Core.ValueObjects.Common
{
    public record NotEmptyString : IValueObject<string>
    {
        public NotEmptyString(string value)
        {
            Value = string.IsNullOrWhiteSpace(value) ? throw new ValueObjectException<NotEmptyString>("String can't be null or empty") : value;
        }

        public string Value { get; private set; }

        public static implicit operator NotEmptyString(string value) => new(value);

        public override string ToString() => Value;
    }
}
