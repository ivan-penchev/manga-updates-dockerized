using MassTransit;
using MU.Common.Messages;
using MU.Translators.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaUpdatedConsumer : IConsumer<MangaUpdatedMessage>
    {
        private readonly ITitleService titleService;

        public MangaUpdatedConsumer(ITitleService titleService)
        {
            this.titleService = titleService;
        }
        public async Task Consume(ConsumeContext<MangaUpdatedMessage> context)
        {
            var title = await this.titleService.FindByMangaId(context.Message.MangaId);
            if(title != null)
            {
                title.PublishedBy = context.Message.Publisher;
                await this.titleService.Save(title);
            }
        }
    }
}
