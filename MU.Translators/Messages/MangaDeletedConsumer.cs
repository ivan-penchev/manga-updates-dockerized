using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaDeletedConsumer : IConsumer<MangaDeletedConsumer>
    {
        public Task Consume(ConsumeContext<MangaDeletedConsumer> context)
        {
            Console.WriteLine(nameof(MangaDeletedConsumer));
            return Task.CompletedTask;
        }
    }
}
