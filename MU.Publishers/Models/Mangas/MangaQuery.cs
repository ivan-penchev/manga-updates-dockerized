using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Mangas
{
    public class MangaQuery
    {
        public int? Genre { get; set; }

        public string Publisher { get; set; }

        public string Title { get; set; }

        public int Page { get; set; } = 1;

    }
}
