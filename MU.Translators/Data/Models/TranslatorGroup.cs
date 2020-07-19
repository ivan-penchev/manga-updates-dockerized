using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Data.Models
{
    public class TranslatorGroup
    {
        public int Id { get; set; }

        public string Name { get; set;}

        public IEnumerable<Translator> Translators { get; set; }

        public IEnumerable<TranslatedTitle> TranslatedTitles { get; set; }
    }
}
