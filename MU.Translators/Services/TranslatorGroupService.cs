using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MU.Common.Services;
using MU.Translators.Data;
using MU.Translators.Data.Models;
using MU.Translators.Models.TranslatorGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Services
{
    public class TranslatorGroupService : DataService<TranslatorGroup>, ITranslatorGroupService
    {
        private readonly IMapper mapper;

        public TranslatorGroupService(TranslatorDbContext db, IMapper mapper) : base(db)
        {
            this.mapper = mapper;
        }

        public async Task<TranslatorGroup> Find(int id)
        => await this.All().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<TranslatorGroup> Find(string groupName)
        => await this.All().FirstOrDefaultAsync(x => x.Name.ToLower().Contains(groupName.ToLower()));

        public async Task<TranslatorGroupOutputModel> GetAll()
        {
            var translatorGroups = await this.All().ToListAsync();
            var outputModel = new TranslatorGroupOutputModel() { TranslatorGroups = translatorGroups};
            return outputModel;
        }
    }
}
