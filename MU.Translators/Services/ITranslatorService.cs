using MU.Common.Services;
using MU.Translators.Data.Models;
using MU.Translators.Models.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Services
{
    public interface ITranslatorService : IDataService<Translator>
    {
        Task Approve(int translatorId);
        Task<TranslatorApplicationOutput> Apply(string userId, int translatorGroupId);
        Task<TranslatorApplicationOutput> GetStatus(string userId, Translator translatorEntity = null);
        
    }
}
