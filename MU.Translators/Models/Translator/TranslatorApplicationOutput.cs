﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Models.Translator
{
    public class TranslatorApplicationOutput
    {
        public int ApplicationId { get; set; }
        public TranslatorApplicationStatus ApplicationStatus { get; set; }
        public DateTime ApplicationReceived { get; set; }
    }
}
