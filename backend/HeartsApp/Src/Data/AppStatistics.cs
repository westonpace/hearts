using System.ComponentModel.DataAnnotations;

namespace Hearts.Application
{
    public class AppStatistics
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public int ShuffleCount { get; set; }
    }
}