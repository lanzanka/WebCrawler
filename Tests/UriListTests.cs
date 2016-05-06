using Core.Crawler;
using Core.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class UriListTests
    {
        private Mock<ICacheHelper> cacheHelper;
        private UriList uriList;

        private readonly Uri uri = new Uri("http://uri.com");
        private const int Depth = 1;

        [SetUp]
        public void Setup()
        {
            cacheHelper = new Mock<ICacheHelper>();
            
            uriList = new UriList(cacheHelper.Object);
        }

        [Test]
        public void IsEmpty_EmptyQueue_ReturnsTrue()
        {
            var result = uriList.IsEmpty();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_EmptyFull_ReturnsFalse()
        {
            var uriQueue = new Queue<Tuple<Uri, int>>();
            uriQueue.Enqueue(new Tuple<Uri, int>(uri, Depth));
            uriList.UriQueue = uriQueue;

            var result = uriList.IsEmpty();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddUris_AllUrisNotProcessed_AddsAllToQueue()
        {
            var uris = new List<Uri> { new Uri("http://a.com"), new Uri("http://b.com") };
            cacheHelper.Setup(x => x.IsInCache(uris[0].ToString(), uris[0])).Returns(false);
            cacheHelper.Setup(x => x.IsInCache(uris[1].ToString(), uris[1])).Returns(false);

            uriList.AddUris(uris, Depth);

            Assert.That(uriList.UriQueue.Count, Is.EqualTo(2));
            Assert.That(uriList.UriQueue.Contains(Tuple.Create(uris[0], Depth)), Is.EqualTo(true));
            Assert.That(uriList.UriQueue.Contains(Tuple.Create(uris[1], Depth)), Is.EqualTo(true));
        }

        [Test]
        public void AddUris_UriAlreadyInCache_AddsToQueue()
        {
            var uris = new List<Uri> { new Uri("http://a.com"), new Uri("http://b.com") };
            cacheHelper.Setup(x => x.IsInCache(uris[0].ToString(), uris[0])).Returns(true);
            cacheHelper.Setup(x => x.IsInCache(uris[1].ToString(), uris[1])).Returns(false);

            uriList.AddUris(uris, Depth);

            Assert.That(uriList.UriQueue.Count, Is.EqualTo(1));
            Assert.That(uriList.UriQueue.Contains(Tuple.Create(uris[0], Depth)), Is.EqualTo(false));
            Assert.That(uriList.UriQueue.Contains(Tuple.Create(uris[1], Depth)), Is.EqualTo(true));
        }
    }
}
