using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MU.Translators.Models.TranslatorGroup;
using MU.Translators.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MU.Translators.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TranslatorGroupController : ControllerBase
    {
        private readonly ITranslatorGroupService translatorGroupService;

        public TranslatorGroupController(ITranslatorGroupService translatorGroupService)
        {
            this.translatorGroupService = translatorGroupService;
        }

        // GET: api/<TranslatorGroupsController>
        [HttpGet]
        [Route(nameof(All))]
        public async Task<ActionResult<TranslatorGroupOutputModel>> All()
        => await this.translatorGroupService.GetAll();
    }
}
