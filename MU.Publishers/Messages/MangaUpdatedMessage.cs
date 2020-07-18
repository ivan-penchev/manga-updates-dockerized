using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Messages
{
    public class MangaUpdatedMessage
    {
        public int MangaId { get; set; }
        public string Publisher { get; set; }
    }
}
