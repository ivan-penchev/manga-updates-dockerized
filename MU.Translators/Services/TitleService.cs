using Microsoft.EntityFrameworkCore;
using MU.Common.Services;
using MU.Translators.Data.Models;
using System.Threading.Tasks;

namespace MU.Translators.Services
{
    public class TitleService : DataService<Title>, ITitleService
    {
        public TitleService(DbContext db) : base(db)
        {
        }

        public async Task<Title> FindById(int id)
        => await this.All()
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Title> FindByMangaId(int id)
        => await this.All()
            .FirstOrDefaultAsync(x => x.TitleId == id);
        public async Task<Title> FindByPublisher(string publisher)
        => await this.All()
            .FirstOrDefaultAsync(x => x.PublishedBy == publisher);
    }
}
