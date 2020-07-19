using MassTransit;
using MU.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaCreatedConsumer : IConsumer<MangaCreatedMessage>
    {
        public Task Consume(ConsumeContext<MangaCreatedMessage> context)
        {
            Console.WriteLine(nameof(MangaCreatedConsumer));
            return Task.CompletedTask;
        }
    }
}
