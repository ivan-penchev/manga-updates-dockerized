using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Data.Models
{
    public class MangaPublisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SiteUrl { get; set; }
        public IEnumerable<Manga> Mangas { get; set; } = new List<Manga>();
    }
}
