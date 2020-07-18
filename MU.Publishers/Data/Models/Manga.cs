using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Data.Models
{
    public class Manga
    {
        public int Id { get; set; }

        public string Author { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public PublishingStatus Status { get; set; }

        public int PublisherId { get; set; }
        public MangaPublisher Publisher { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int? LastEditedByUserId { get; set; }

    }
}
