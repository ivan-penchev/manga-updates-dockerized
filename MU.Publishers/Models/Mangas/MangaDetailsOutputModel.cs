using AutoMapper;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Mangas
{
    public class MangaDetailsOutputModel : MangaOutputModel
    {
        public string Description { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public int? LastEditedByUserId { get; set; }
        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<Manga, MangaDetailsOutputModel>()
                .IncludeBase<Manga, MangaOutputModel>();
    }
}
