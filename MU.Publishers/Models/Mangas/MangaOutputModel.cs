using AutoMapper;
using MU.Common.Models;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Mangas
{
    public class MangaOutputModel : IMapFrom<Manga>
    {
        public int Id { get; set; }

        public string Author { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public string Publisher { get; set; }
        public string Genre { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual void Mapping(Profile mapper)
           => mapper
               .CreateMap<Manga, MangaOutputModel>()
               .ForMember(ad => ad.Publisher, cfg => cfg
                   .MapFrom(ad => ad.Publisher.Name))
               .ForMember(ad => ad.Genre, cfg => cfg
                   .MapFrom(ad => ad.Genre.Name))
              .ForMember(ad => ad.Status, cfg => cfg
                   .MapFrom(ad => ad.Status.ToString()));
    }
}
