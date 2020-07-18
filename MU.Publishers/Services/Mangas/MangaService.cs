using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MU.Common.Services;
using MU.Publishers.Data;
using MU.Publishers.Data.Models;
using MU.Publishers.Models.Mangas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Services.Mangas
{
    public class MangaService : DataService<Manga>, IMangaService
    {
        private const int MangasPerPage = 10;
        private readonly IMapper mapper;

        public MangaService(PublishersDbContext db, IMapper mapper) : base(db)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            var manga = await this.Data.FindAsync<Manga>(id);

            if (manga == null)
            {
                return false;
            }

            this.Data.Remove(manga);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<Manga> Find(int id)
        => await this.All().FirstOrDefaultAsync(c => c.Id == id);

        public async Task<MangaDetailsOutputModel> GetDetails(int id)
        => await this.mapper
                .ProjectTo<MangaDetailsOutputModel>(this
                    .All()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync();


        public async Task<IEnumerable<MangaOutputModel>> GetListings(MangaQuery query)
        =>
           (await this.mapper
                .ProjectTo<MangaOutputModel>(this
                    .GetMangasQuery(query))
                .ToListAsync())
            .Skip((query.Page - 1) * MangasPerPage)
            .Take(MangasPerPage); //CLIENT SIDE BLYH :(
        

        public async Task<int> TotalMangas(MangaQuery query)
        => await this
                .GetMangasQuery(query)
                .CountAsync();
        public async Task<int> TotalPages(MangaQuery query)
        {
            var totalMangas = await this.TotalMangas(query);
            if (totalMangas == 0)
                return 1;
            if ((totalMangas / MangasPerPage) <= 0)
                return 1;
            return (totalMangas / MangasPerPage);
        }
   

        private IQueryable<Manga> GetMangasQuery(
          MangaQuery query)
        {
            var dataQuery = this.All();


            if (query.Genre.HasValue)
            {
                dataQuery = dataQuery.Where(c => c.GenreId == query.Genre);
            }

            if (!string.IsNullOrWhiteSpace(query.Publisher))
            {
                dataQuery = dataQuery.Where(c => c
                    .Publisher.Name.ToLower().Contains(query.Publisher.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                dataQuery = dataQuery.Where(c => c.Title.ToLower().Contains(query.Title.ToLower()));
            }

            return dataQuery;
        }
    }
}
