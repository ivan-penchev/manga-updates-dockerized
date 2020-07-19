using MU.Common.Services;
using MU.Translators.Data.Models;
using System.Threading.Tasks;

namespace MU.Translators.Services
{
    public interface ITitleService : IDataService<Title>
    {
        Task<Title> FindById(int id);
        Task<Title> FindByMangaId(int id);
        Task<Title> FindByPublisher(string publisher);
    }
}
