using System.Threading.Tasks;

namespace Hearts.Application
{
    public interface ILiveMigration
    {
        string Name { get; }

        Task MigrateAsync(HeartsContext context);
    }
}