using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Hearts
{
    public class ShufflerTests : BaseTest
    {

        private List<PlayingCard> cards = new List<PlayingCard>()
        {
            new PlayingCard { Suit = Suit.Clubs, Rank = 3 },
            new PlayingCard { Suit = Suit.Spades, Rank = 8 },
            new PlayingCard { Suit = Suit.Hearts, Rank = 10 },
            new PlayingCard { Suit = Suit.Clubs, Rank = 1 },
        };
        private Shuffler shuffler;

        public ShufflerTests()
        {
            shuffler = new Shuffler(random);
        }

        [Test]
        public void ShouldShuffleCards()
        {
            var copy = new List<PlayingCard>(cards);
            shuffler.Shuffle(copy);
            Assert.AreEqual(cards[1], copy[0]);
            Assert.AreEqual(cards[3], copy[1]);
            Assert.AreEqual(cards[0], copy[2]);
            Assert.AreEqual(cards[2], copy[3]);
        }

    }
}