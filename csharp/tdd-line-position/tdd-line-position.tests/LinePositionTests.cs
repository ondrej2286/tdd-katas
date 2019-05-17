using NUnit.Framework;
using Shouldly;
using System;

namespace TddLinePosition.Tests
{
    [TestFixture]
    public class LinePositionTests
    {
        [Test]
        public void LinePosition_IsStruct()
        {
            typeof(LinePosition).IsValueType.ShouldBeTrue();
        }

        [Test]
        public void Constructor_WhenInvokedWithTwoIntArguments_DoesNotThrow()
        {
            Action creatingLinePosition = () => new LinePosition(1, 1);

            creatingLinePosition.ShouldNotThrow();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Constructor_WhenLineIsZeroOrNegative_ThrowsArgumentOutOfRangeException(int line)
        {
            Action createLinePosition = () => new LinePosition(line, 1);

            createLinePosition.ShouldThrow<ArgumentOutOfRangeException>();
        }
        

        [TestCase(0)]
        [TestCase(-1)]
        public void Constructor_WhenColumnIsZeroOrNegative_ThrowsArgumentOutOfRangeException(int column)
        {
            Action createLinePosition = () => new LinePosition(1, column);

            createLinePosition.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestCase(13)]
        [TestCase(14)]
        public void Line_ReturnsLineSetInConstructor(int line)
        {
            var position = new LinePosition(line, 2);

            position.Line.ShouldBe(line);
        }

        [TestCase(13)]
        [TestCase(14)]
        public void Column_ReturnsColumnSetInConstructor(int column)
        {
            const int ANY_VALID_INT = 2;

            var position = new LinePosition(ANY_VALID_INT, column);

            position.Column.ShouldBe(column);
        }

        [TestCase(2,3)]
        [TestCase(4, 5)]
        public void Position_ShouldReturnLineColumnSeparatedByColon_WhenToStringIsInvoked(int line, int column)
        {
            var position = new LinePosition(line, column);
            var expectedOutput = line + ":" + column;

            var result = position.ToString();

            result.ShouldBe(expectedOutput);
        }

        [Test]
        public void TryParse_ReturnsFalse_WhenStringIsNull()
        {
            var result = LinePosition.TryParse(null, out _);

            result.ShouldBeFalse();
        }

        [TestCase("4:4")]
        public void TryParse_ReturnsTrue_WhenStringIsValidLinePosition(string linePosition)
        {
            var result = LinePosition.TryParse(linePosition, out _);

            result.ShouldBeTrue();
        }

        [TestCase("1:3", 1)]
        [TestCase("12:3", 12)]
        public void TryParse_SetsCorrectLine_WhenStringIsValid(string text, int expectedLine)
        {
            LinePosition.TryParse(text, out var linePosition);

            linePosition.Line.ShouldBe(expectedLine);
;       }
    }
}