using AutoMapper;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MU.Publishers.Models.MangaPublishers
{
    public class MangaPublisherDetailsOutputModel : MangaPublisherOutputModel
    {
        public int TotalMangaTitlePublished { get; private set; }
        public List<String> MangaTitlesPublished { get; private set; }

        public void Mapping(Profile mapper)
           => mapper
               .CreateMap<MangaPublisher, MangaPublisherDetailsOutputModel>()
               .IncludeBase<MangaPublisher, MangaPublisherOutputModel>()
               .ForMember(d => d.TotalMangaTitlePublished, cfg => cfg
                   .MapFrom(d => d.Mangas.Count()))
               .ForMember(d => d.MangaTitlesPublished, cfg => cfg
                   .MapFrom(d => d.Mangas.Select(x=>x.Title).ToList()));
    }
}
