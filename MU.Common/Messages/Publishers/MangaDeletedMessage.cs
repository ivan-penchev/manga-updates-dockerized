﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Common.Messages
{
    public class MangaDeletedMessage
    {
        public int MangaId { get; set; }
        public string Publisher { get; set; }
    }
}
