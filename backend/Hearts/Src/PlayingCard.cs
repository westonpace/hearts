using System;
using System.Collections.Generic;

namespace Hearts
{
    public class PlayingCard
    {
        public const int MaxRank = 13;
        public const int MinRank = 1;

        public int Rank { get; set; }
        public Suit Suit { get; set; }

        public static List<PlayingCard> NewDeck()
        {
            var result = new List<PlayingCard>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (var rank = MinRank; rank <= MaxRank; rank++)
                {
                    result.Add(new PlayingCard { Rank = rank, Suit = suit });
                }
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var casted = (PlayingCard) obj;
            
            return Rank == casted.Rank && Suit == casted.Suit;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Rank, Suit);
        }

    }

}