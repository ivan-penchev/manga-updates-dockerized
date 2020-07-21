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

        public async Task Approve(Translator translator)
        {
            translator.ApprovedByAdmin = true;
            translator.DateApprovedByAdmin = DateTime.Now;
            await this.Save(translator);
        }

        public async Task<Translator> Find(int translatorId)
        => await this.All().FirstOrDefaultAsync(x => x.Id == translatorId);

        public async Task<List<TranslatorApplicationOutput>> GetAllUnapprovedApplications()
        {
            var applicationsToReturn = new List<TranslatorApplicationOutput>();
            var applicationsForReview = await this.All().Where(x => x.ApprovedByAdmin == false).ToListAsync();
            if (applicationsForReview.Count > 0)
                applicationsForReview.ForEach(x => applicationsToReturn.Add(this.GenerateApplication(x))); //Efficient code is efficient :D

            return applicationsToReturn;
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
                ApplicationId = translator.Id,
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
