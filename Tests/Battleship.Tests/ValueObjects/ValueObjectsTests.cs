using Battleship.Core.ValueObjects.Common;
using Battleship.Core.ValueObjects.Exceptions;
using FluentAssertions;

namespace Battleship.Tests.ValueObjects;

public class ValueObjectsTests
{
    [Fact]
    public void Create_NonNegativeNumber_ExceptionThrown()
    {
        var action = () => new NonNegativeNumber(-1);
        action.Should().Throw<ValueObjectException<NonNegativeNumber>>().WithMessage("Given number can't be negative");
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Create_NonNegativeNumber_Valid(int value)
    {
        var action = () => new NonNegativeNumber(value);
        action.Should().NotThrow();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_NotEmptyString_ExceptionThrown(string value)
    {
        var action = () => new NotEmptyString(value);

        action.Should().Throw<ValueObjectException<NotEmptyString>>().WithMessage("String can't be null or empty");
    }
    
    [Theory]
    [InlineData("123")]
    [InlineData("test")]
    public void Create_NotEmptyString_Valid(string value)
    {
        var action = () => new NotEmptyString(value);
        action.Should().NotThrow();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_PositiveNumber_ExceptionThrown(int value)
    {
        var action = () => new PositiveNumber(value);
        action.Should().Throw<ValueObjectException<PositiveNumber>>().WithMessage("Given number can't be negative or equal to zero");
    }
    
    [Fact]
    public void Create_PositiveNumber_Valid()
    {
        var action = () => new PositiveNumber(1);
        action.Should().NotThrow();
    }
}