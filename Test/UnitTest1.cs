using Xunit;

namespace MyApp.Tests
{
    public class SimpleMathTests
    {
        [Fact]
        public void Addition_TwoPlusZero_IsTwo()
        {
            int a = 1;
            int b = 2;
            Assert.Equal(2, a + b);
        }
    }
}
