using MassTransit;
using Microsoft.AspNetCore.Identity;
using MU.Common.Messages.Translator;
using MU.Identity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MU.Common.Constants;

namespace MU.Identity.Messages.Consumers
{
    public class TranslatorApprovedConsumer : IConsumer<TranslatorApprovedMessage>
    {
        private readonly UserManager<User> userManager;

        public TranslatorApprovedConsumer(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Consume(ConsumeContext<TranslatorApprovedMessage> context)
        {
            var user = await this.userManager.FindByIdAsync(context.Message.UserId);
            if(user != null)
            {
                await this.userManager.AddToRoleAsync(user, TranslatorRoleName);
            }
        }
    }
}
