using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Models
{
    public class Title
    {
        public int Id { get; set; }
        public string TitleId { get; set; }
        public string PublishedBy { get; set; }
        
        public IEnumerable<TranslatedTitle> Translations { get; set; }
    }
}
