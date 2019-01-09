using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hearts.Application
{
    public class ShuffleTest : ApiTest
    {
        [Test]
        public async Task ShouldCreateShuffledDeck()
        {
            var response = await GetAsync<ShuffledDeckWithCount>("/api");
            Assert.AreEqual(52, response.Cards.Count);
            Assert.AreNotEqual(1, response.Cards[0].Rank);
            Assert.AreEqual(1, response.ShuffleCount);
            response = await GetAsync<ShuffledDeckWithCount>("/api");
            Assert.AreEqual(2, response.ShuffleCount);
        }
    }
}