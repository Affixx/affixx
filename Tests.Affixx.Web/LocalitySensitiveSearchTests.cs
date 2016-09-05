using Affixx.Web.Services.Algorithms;
using NUnit.Framework;
using System;

namespace Tests.Affixx.Web
{
    [TestFixture]
    public class LocalitySensitiveSearchTests
    {
        [Test]
        public void Process()
        {
            var document1 = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.";
            var document2 = "Contrary to the popular belief Lorem Ipsum is not a random text, but has roots in classical Latin literature, which is over 2000 years old. A Latin professor found the word 'consectetur' from a Lorem Ipsum text in classical literature, discovered the undoubtable source.";
            var searcher = new LocalitySensitiveSearch();

            var signature1 = searcher.GetSignature(1, document1);
            var signature2 = searcher.GetSignature(2, document2);

            //var similarities = signature1.GetSimilarDocuments(signature2);

            //Console.WriteLine("\n" + string.Join(",", similarities));

            //Assert.That(similarities[0], Is.EqualTo(1));
            //Assert.That(similarities[0], Is.AtLeast(0.2));
        }
    }
}
