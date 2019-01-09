using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hearts.Application
{
    public class ShuffledDeckWithCount
    {
        public int ShuffleCount { get; set; }
        public List<PlayingCard> Cards { get; set; }
    }

    public class GetShuffledDeckUseCase
    {
        private Shuffler shuffler;
        private AppStatisticsRepository appStatisticsRepository;

        public GetShuffledDeckUseCase(Shuffler shuffler, AppStatisticsRepository appStatisticsRepository)
        {
            this.shuffler = shuffler;
            this.appStatisticsRepository = appStatisticsRepository;
        }

        public async Task<ShuffledDeckWithCount> Go()
        {
            var cards = PlayingCard.NewDeck();
            shuffler.Shuffle(cards);
            var shuffleCount = await appStatisticsRepository.IncrementShuffleCount();
            return new ShuffledDeckWithCount
            {
                ShuffleCount = shuffleCount,
                Cards = cards
            };
        }
    }
}