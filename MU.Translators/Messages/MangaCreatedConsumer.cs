using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Translators.Messages
{
    public class MangaCreatedConsumer : IConsumer<MangaCreatedConsumer>
    {
        public Task Consume(ConsumeContext<MangaCreatedConsumer> context)
        {
            Console.WriteLine(nameof(MangaCreatedConsumer));
            return Task.CompletedTask;
        }
    }
}
