
using MU.Publishers.Data.Models;
using MU.Publishers.Models.Genres;
using System;
using System.Collections.Generic;
using MU.Common.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MU.Publishers.Data;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace MU.Publishers.Services.Genres
{
    public class GenreService : DataService<Genre>, IGenreService
    {
        private readonly IMapper mapper;

        public GenreService(PublishersDbContext db, IMapper mapper) : base(db)
        {
            this.mapper = mapper;
        }

        public async Task<Genre> Find(int genreId)
        => await this.Data.FindAsync<Genre>(genreId);

        public async Task<Genre> Find(string genreName)
        => await this.All()
            .FirstOrDefaultAsync(g => g.Name == genreName);

      
        public async Task<IEnumerable<GenreOutputModel>> GetAll()
        {
           return await this.mapper
                .ProjectTo<GenreOutputModel>(this.All())
                .ToListAsync();
        }
    }
}
