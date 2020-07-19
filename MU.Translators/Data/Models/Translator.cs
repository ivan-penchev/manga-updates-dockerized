using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Models
{
    public class Translator
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime TranslatorRegistered { get; set; }
        public bool ApprovedByAdmin { get; set; }

        public DateTime? DateApprovedByAdmin { get; set; }

        public int TranslatorGroupId { get; set; }
        public TranslatorGroup TranslatorGroup { get; set; }

        public IEnumerable<TranslatedTitle> Translations { get; set; }

    }
}
