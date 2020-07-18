using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Mangas
{
    public abstract class MangasOutputModel<TMangaOutputModel>
    {
        protected MangasOutputModel(
            IEnumerable<TMangaOutputModel> mangas,
            int page,
            int totalPages)
        {
            this.Mangas = mangas;
            this.Page = page;
            this.TotalPages = totalPages;
        }

        public IEnumerable<TMangaOutputModel> Mangas { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}
