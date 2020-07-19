using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Models
{
    public class TranslatedTitle
    {
        public int Id { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }
        public int TranslatorId { get; set; }
        public Translator Translator { get; set; }
        public int TranslatorGroupId { get; set; }
        public TranslatorGroup TranslatorGroup {get; set;}
        public DateTime DateTranslated { get; set; }
        public bool Visible { get; set; }
    }
}
