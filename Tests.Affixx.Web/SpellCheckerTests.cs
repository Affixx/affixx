using Affixx.Web.Services;
using NUnit.Framework;

namespace Tests.Affixx.Web
{
    [TestFixture]
    public class SpellServiceTests
    {
        [Test]
        [TestCase("sppelled wronggly", true, "spelled wrongly")]
        [TestCase("everything is good", false, "everything is good")]
        [TestCase("uber", false, "uber")] //we have documents about Uber the company, should not treat the name as typo
        public void Process(string input, bool expectedToCorrect, string expectedOutput)
        {
            var service = new SpellService();
            var words = input.Split(' ');
            var expectedWords = expectedOutput.Split(' ');

            bool corrected;
            var correctedWords = service.CorrectSpelling(words, out corrected);

            Assert.That(corrected, Is.EqualTo(expectedToCorrect));
            Assert.That(correctedWords, Is.EquivalentTo(expectedWords));
        }
    }
}
