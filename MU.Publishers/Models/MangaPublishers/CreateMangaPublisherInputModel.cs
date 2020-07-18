using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MU.Publishers.Models.MangaPublishers
{
    public class CreateMangaPublisherInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MinLength(15)]
        [MaxLength(350)]
        [RegularExpression("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$")]
        public string SiteUrl { get; set; }
    }
}
