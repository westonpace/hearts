using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Hearts
{
    public class PlayingCardTests
    {

        [TestFixture]
        public class TestsWithADeck : PlayingCardTests
        {

            private List<PlayingCard> deck;

            [SetUp]
            public void Setup()
            {
                deck = PlayingCard.NewDeck();
            }

            [Test]
            public void ShouldContain52Cards()
            {
                Assert.AreEqual(52, deck.Count);
            }

            [Test]
            public void ShouldContain13OfEachSuit()
            {
                foreach(Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    var count = deck.Where(card => card.Suit == suit).Count();
                    Assert.AreEqual(13, count);
                }
            }

            [Test]
            public void ShouldContain4OfEachRank()
            {
                for(int rank = PlayingCard.MinRank; rank <= PlayingCard.MaxRank; rank++)
                {
                    var count = deck.Where(card => card.Rank == rank).Count();
                    Assert.AreEqual(4, count);
                }
            }

        }
    }
}