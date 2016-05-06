using Core.Helpers;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class UriHelperTests
    {
        private static readonly Uri baseUri = new Uri("http://www.baseuri.com");

        private readonly UriHelper uriHelper = new UriHelper();

        [Test]
        [TestCase("relative/url", "http://www.baseuri.com/relative/url")]
        [TestCase("http://www.absolute.url", "http://www.absolute.url/")]
        public void GetProperUri_ForWellFormedUriString_ReturnsUri(string url, string expected)
        {
            var result = uriHelper.GetProperUri(baseUri, url);

            Assert.That(result.ToString(), Is.EqualTo(expected));
        }
    }
}

