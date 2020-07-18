using MU.Common.Services;
using MU.Publishers.Data.Models;
using MU.Publishers.Models.Mangas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Services.Mangas
{
    public interface IMangaService: IDataService<Manga>
    {
        Task<Manga> Find(int id);

        Task<bool> Delete(int id);

        Task<IEnumerable<MangaOutputModel>> GetListings(MangaQuery query);

        Task<MangaDetailsOutputModel> GetDetails(int id);

        Task<int> TotalMangas(MangaQuery query);
        Task<int> TotalPages(MangaQuery query);
    }
}
