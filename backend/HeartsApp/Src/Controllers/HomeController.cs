using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hearts.Application
{
    [Route("/api")]
    public class HomeController : Controller
    {

        private GetShuffledDeckUseCase getShuffledDeckUseCase;

        public HomeController(
            GetShuffledDeckUseCase getShuffledDeckUseCase
        ) {
            this.getShuffledDeckUseCase = getShuffledDeckUseCase;
        }

        [HttpGet]
        public Task<ShuffledDeckWithCount> GetShuffledDeck()
        {
            return this.getShuffledDeckUseCase.Go();
        }

    }
}