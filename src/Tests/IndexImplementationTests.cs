namespace Tests
{
    using ParseCommandLineArguments;
    using System.Linq;
    using Xunit;
    using Xunit.Sdk;

    public class IndexImplementationTests
    {
        [Fact]
        public void VerifyIndexIsRight()
        {
            var input = " arg0   arg1  \"arg2 arg2\"  arg3 ";
            var result = ParseCommandLineArguments.Mikescher_PlusIndexImpl(input).ToArray();

            Assert.Equal(1, result[0].Key);
            Assert.Equal(8, result[1].Key);
            Assert.Equal(15, result[2].Key);
            Assert.Equal(27, result[3].Key);
        }
    }
}
