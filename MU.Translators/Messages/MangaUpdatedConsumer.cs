using MassTransit;
using MU.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaUpdatedConsumer : IConsumer<MangaUpdatedMessage>
    {
        public Task Consume(ConsumeContext<MangaUpdatedMessage> context)
        {
            Console.WriteLine(nameof(MangaUpdatedConsumer));
            return Task.CompletedTask;
        }
    }
}
