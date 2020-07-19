using MassTransit;
using MU.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaDeletedConsumer : IConsumer<MangaDeletedMessage>
    {
        public Task Consume(ConsumeContext<MangaDeletedMessage> context)
        {
            Console.WriteLine(nameof(MangaDeletedConsumer));
            return Task.CompletedTask;
        }
    }
}
