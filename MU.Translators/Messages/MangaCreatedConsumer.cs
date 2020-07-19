using MassTransit;
using MU.Common.Messages;
using MU.Translators.Data.Models;
using MU.Translators.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaCreatedConsumer : IConsumer<MangaCreatedMessage>
    {
        private readonly ITitleService titleService;

        public MangaCreatedConsumer(ITitleService titleService)
        {
            this.titleService = titleService;
        }

        public async Task Consume(ConsumeContext<MangaCreatedMessage> context)
        {
            var title = new Title { 
                PublishedBy = context.Message.Publisher, 
                TitleId = context.Message.MangaId 
            };
            await this.titleService.Save(title);
        }
    }
}
