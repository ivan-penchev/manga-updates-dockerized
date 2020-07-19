using MU.Common.Services;
using MU.Translators.Data.Models;
using MU.Translators.Models.TranslatorGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Services
{
    public interface ITranslatorGroupService: IDataService<TranslatorGroup>
    {
        Task<TranslatorGroup> Find(int id);
        Task<TranslatorGroup> Find(string groupName);
        Task<TranslatorGroupOutputModel> GetAll();
    }
}
