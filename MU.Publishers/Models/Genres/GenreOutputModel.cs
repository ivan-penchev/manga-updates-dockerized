using AutoMapper;
using MU.Common.Models;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Genres
{
    public class GenreOutputModel : IMapFrom<Genre>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public int TotalMangaTitles { get; set; }

        public void Mapping(Profile profile)
            => profile
                .CreateMap<Genre, GenreOutputModel>()
                .ForMember(c => c.TotalMangaTitles, cfg => cfg
                    .MapFrom(c => c.Mangas.Count()));
    }
}
