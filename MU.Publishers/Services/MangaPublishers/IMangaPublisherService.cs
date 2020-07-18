using MU.Common.Services;
using MU.Publishers.Data.Models;
using MU.Publishers.Models.MangaPublishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Services.MangaPublishers
{
    public interface IMangaPublisherService: IDataService<MangaPublisher>
    {
        Task<MangaPublisher> Find(int id);
        Task<MangaPublisher> Find(string name);
        Task<MangaPublisherOutputModel> GetMangaPublishersDetails(int id);
        Task<MangaPublisherDetailsOutputModel> GetMangaPublishersFullDetails(int id);
        Task<IEnumerable<MangaPublisherDetailsOutputModel>> GetAllMangaPublishers();
    }
}
