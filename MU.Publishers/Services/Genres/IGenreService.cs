using MU.Common.Services;
using MU.Publishers.Data.Models;
using MU.Publishers.Models.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Services.Genres
{
    public interface IGenreService: IDataService<Genre>
    {
        Task<Genre> Find(int genreId);
        Task<Genre> Find(string genreName);
        Task<IEnumerable<GenreOutputModel>> GetAll();
    }
}
