using MU.Common.Models;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.MangaPublishers
{
    public class MangaPublisherOutputModel : IMapFrom<MangaPublisher>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SiteUrl { get; set; }
    }
}
