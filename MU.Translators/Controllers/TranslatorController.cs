using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MU.Common.Infrastructure;
using MU.Common.Messages.Translator;
using MU.Common.Services.Identity;
using MU.Translators.Data.Models;
using MU.Translators.Models.Translator;
using MU.Translators.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MU.Translators.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ITranslatorService translatorService;
        private readonly ITranslatorGroupService translatorGroupService;
        private readonly IBus publisher;

        public TranslatorController(ICurrentUserService currentUserService,
            ITranslatorService translatorService,
            ITranslatorGroupService translatorGroupService, 
            IBus publisher)
        {
            this.currentUserService = currentUserService;
            this.translatorService = translatorService;
            this.translatorGroupService = translatorGroupService;
            this.publisher = publisher;
        }

        // GET: api/<TranslatorController>
        [HttpPost]
        [Route(nameof(Apply))]
        [Authorize]
        public async Task<ActionResult<TranslatorApplicationOutput>> Apply([FromBody] TranslatorApplicationInput input)
        {
            var user = this.currentUserService.UserId;
            var group = await this.translatorGroupService.Find(input.PartOfTranslatorGroup);

            if(string.IsNullOrWhiteSpace(user)
                || group == null)
                return BadRequest();

            var result = await this.translatorService.Apply(user, group.Id);
            return result;
        }

        // GET api/<TranslatorController>/5
        [HttpGet]
        [Route(nameof(Status))]
        [Authorize]
        public async Task<TranslatorApplicationOutput> Status()
        => await this.translatorService.GetStatus(this.currentUserService.UserId);

        [HttpGet]
        [Route(nameof(Review))]
        [AuthorizeAdministrator]
        public async Task<IEnumerable<TranslatorApplicationOutput>> Review()
        => await this.translatorService.GetAllUnapprovedApplications();

        // GET: api/<TranslatorController>
        [HttpPost]
        [Route(nameof(Approve) + "/{id}")]
        [AuthorizeAdministrator]
        public async Task<ActionResult<bool>> Approve(int id)
        {
            var translatorApplication = await this.translatorService.Find(id);
            if (translatorApplication == null)
                return false;
            await this.translatorService.Approve(translatorApplication);
            await this.publisher.Publish(new TranslatorApprovedMessage { UserId = translatorApplication.UserId });
            return true;
        }
       
    }
}
