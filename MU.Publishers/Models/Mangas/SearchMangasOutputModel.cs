using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Mangas
{
    public class SearchMangasOutputModel : MangasOutputModel<MangaOutputModel>
    {
        public SearchMangasOutputModel(IEnumerable<MangaOutputModel> mangas, int page, int totalPages) : base(mangas, page, totalPages)
        {
        }
    }
}
