using FlexRule.Services;
using Xunit;

namespace FlexRuleTest
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Test_ReverseString()
        {
            //Arrange
            string initialText = "cat and dog";

            string expectedResult = "tac dna god";

            // Act
            string actualResult = initialText.ReverseWords();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Test_ReverseString_withextraspace()
        {
            //Arrange
            string initialText = "cat and dog ";

            string expectedResult = "tac dna god ";

            // Act
            string actualResult = initialText.ReverseWords();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Test_ReverseString_withspecialchars()
        {
            //Arrange
            string initialText = "  It's working.   ";

            string expectedResult = "  s'tI .gnikrow   ";

            // Act
            string actualResult = initialText.ReverseWords();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
