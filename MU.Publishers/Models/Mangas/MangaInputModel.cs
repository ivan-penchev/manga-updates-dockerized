using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Models.Mangas
{
    public class MangaInputModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(PublishingStatus))]
        public PublishingStatus Status { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Publisher { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompleteDate { get; set; }

    }
}
