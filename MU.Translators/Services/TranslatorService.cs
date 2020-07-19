using Microsoft.EntityFrameworkCore;
using MU.Common.Services;
using MU.Translators.Data;
using MU.Translators.Data.Models;
using MU.Translators.Models.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Services
{
    public class TranslatorService : DataService<Translator>, ITranslatorService
    {
        public TranslatorService(TranslatorDbContext db) : base(db)
        {
        }

        public async Task<TranslatorApplicationOutput> Apply(string userId, int translatorGroupId)
        {
            var previousApplication = await this.All().FirstOrDefaultAsync(x => x.UserId == userId);
            if (previousApplication == null)
            {
                var translator = new Translator
                {
                    TranslatorGroupId = translatorGroupId,
                    UserId = userId
                };

                this.Data.Add(translator);
                await this.Data.SaveChangesAsync();

                previousApplication = translator;
            }

            return await this.GetStatus(userId, previousApplication);
        }

        public Task Approve(int translatorId)
        {
            throw new NotImplementedException();
        }

        public async Task<TranslatorApplicationOutput> GetStatus(string userId, Translator translatorEntity = null)
        {
            Translator translator = translatorEntity;
            if (translatorEntity == null)
                translator = await this.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return GenerateApplication(translator);
        }

        private TranslatorApplicationOutput GenerateApplication(Translator translator)
        {
            var application = new TranslatorApplicationOutput { 
                ApplicationStatus = TranslatorApplicationStatus.NotApplied,
                ApplicationReceived = DateTime.Now 
            };

            if(translator != null)
            {
                application.ApplicationStatus = translator.ApprovedByAdmin?
                    TranslatorApplicationStatus.Approved
                    : TranslatorApplicationStatus.UnderReview;
                application.ApplicationReceived = translator.TranslatorRegistered;
            };
            return application;
        }
    }
}
