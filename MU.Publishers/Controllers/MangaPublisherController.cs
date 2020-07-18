using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MU.Publishers.Models.MangaPublishers;
using MU.Publishers.Services.MangaPublishers;
using MU.Publishers.Data.Models;
using MU.Common.Services;
using System;
using System.Collections.Generic;
using MU.Common.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MU.Publishers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangaPublisherController : ControllerBase
    {
        private readonly IMangaPublisherService mangaPublisherService;

        public MangaPublisherController(IMangaPublisherService mangaPublisherService)
        {
            this.mangaPublisherService = mangaPublisherService;
        }

        // GET api/<MangaPublisherController>/5
        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult<MangaPublisherOutputModel>> Details(int id)
            => await this.mangaPublisherService.GetMangaPublishersDetails(id);


        // POST api/<MangaPublisherController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] CreateMangaPublisherInputModel input)
        {
            try
            {
                await this.mangaPublisherService.Save(new MangaPublisher()
                {
                    Name = input.Name,
                    SiteUrl = input.SiteUrl
                });
            }
            catch (Exception e) { return Result.Failure(e.Message); }

            return Ok();
        }

        // PUT api/<MangaPublisherController>/5
        [HttpPut()]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(int id, [FromBody] EditMangaPublisherInputModel input)
        {
            var currentMangaPublisher = await this.mangaPublisherService.Find(id);
            if (currentMangaPublisher == null)
                return BadRequest();

            currentMangaPublisher.Name = input.Name;
            currentMangaPublisher.SiteUrl = input.SiteUrl;
            await this.mangaPublisherService.Save(currentMangaPublisher);

            return Ok();
        }

        [HttpGet]
        [AuthorizeAdministrator]
        public async Task<IEnumerable<MangaPublisherDetailsOutputModel>> All()
            => await this.mangaPublisherService.GetAllMangaPublishers();

    }
}
