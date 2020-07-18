using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MU.Common.Services;
using MU.Publishers.Data;
using MU.Publishers.Data.Models;
using MU.Publishers.Models.MangaPublishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MU.Publishers.Services.MangaPublishers
{
    public class MangaPublisherService : DataService<MangaPublisher>, IMangaPublisherService
    {
        private readonly IMapper mapper;

        public MangaPublisherService(PublishersDbContext db, IMapper mapper) : base(db)
        {
            this.mapper = mapper;
        }

        public async Task<MangaPublisher> Find(int id)
        => await this.Data.FindAsync<MangaPublisher>(id);

        public async Task<IEnumerable<MangaPublisherDetailsOutputModel>> GetAllMangaPublishers()
        => await this.mapper.ProjectTo<MangaPublisherDetailsOutputModel>(this.All().Include(x => x.Mangas)).ToListAsync();

        public async Task<MangaPublisherOutputModel> GetMangaPublishersDetails(int id)
        => await this.mapper.ProjectTo<MangaPublisherOutputModel>(this.All().Where(x => x.Id == id)).SingleOrDefaultAsync();

        public async Task<MangaPublisherDetailsOutputModel> GetMangaPublishersFullDetails(int id)
        => await this.mapper.ProjectTo<MangaPublisherDetailsOutputModel>(this.All().Where(x => x.Id == id).Include(x => x.Mangas)).SingleOrDefaultAsync();
    }
}
