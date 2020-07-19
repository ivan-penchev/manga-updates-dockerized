using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TranslatorController(ICurrentUserService currentUserService,
            ITranslatorService translatorService,
            ITranslatorGroupService translatorGroupService)
        {
            this.currentUserService = currentUserService;
            this.translatorService = translatorService;
            this.translatorGroupService = translatorGroupService;
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
    }
}
