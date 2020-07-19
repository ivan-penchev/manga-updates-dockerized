using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MU.Common.Messages;
using MU.Common.Services.Identity;
using MU.Publishers.Data.Models;
using MU.Publishers.Models.Mangas;
using MU.Publishers.Services.Genres;
using MU.Publishers.Services.MangaPublishers;
using MU.Publishers.Services.Mangas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MU.Publishers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly IMangaService mangaService;
        private readonly IBus publisher;
        private readonly IGenreService genreService;
        private readonly IMangaPublisherService mangaPublisherService;
        private readonly ICurrentUserService currentUserService;

        public MangaController(IMangaService mangaService, 
            IBus publisher, 
            IGenreService genreService,
            IMangaPublisherService mangaPublisherService,
            ICurrentUserService currentUserService)
        {
            this.mangaService = mangaService;
            this.publisher = publisher;
            this.genreService = genreService;
            this.mangaPublisherService = mangaPublisherService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<SearchMangasOutputModel>> Search(
            [FromQuery] MangaQuery query)
        {
            var totalPages = await this.mangaService.TotalPages(query);

            if (query.Page > totalPages)
                query.Page = totalPages;

            var mangaListings = await this.mangaService.GetListings(query);

            return new SearchMangasOutputModel(mangaListings, query.Page, totalPages);
        }

        // GET api/<MangaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MangaDetailsOutputModel>> Details(int id)
            => await this.mangaService.GetDetails(id);

        // POST api/<MangaController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] MangaInputModel input)
        {
            var genre = await this.genreService.Find(input.Genre);
            var publisher = await this.mangaPublisherService.Find(input.Publisher);
            if(genre == null || publisher == null)
            {
                return BadRequest();
            }

            var manga = new Manga
            {
                Genre = genre,
                Publisher = publisher,
                Title = input.Title,
                Author = input.Author,
                Description = input.Description,
                CompleteDate = input.CompleteDate,
                StartDate = input.StartDate,
                Status = input.Status
            };

            var mangaMessage = new MangaCreatedMessage
            {
                MangaId = manga.Id,
                Publisher = manga.Publisher.Name
            };

            await this.mangaService.Save(manga);
            await this.publisher.Publish(mangaMessage);

            return Ok();
        }

        // PUT api/<MangaController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, [FromBody] MangaInputModel input)
        {
            var genre = await this.genreService.Find(input.Genre);
            var publisher = await this.mangaPublisherService.Find(input.Publisher);
            var manga = await this.mangaService.Find(id);
            if (genre == null 
                || publisher == null
                || manga == null)
            {
                return BadRequest();
            }
            manga.Genre = genre;
            manga.Publisher = publisher;
            manga.Title = string.IsNullOrWhiteSpace(input.Title)
                            ? manga.Title : input.Title;
            manga.Author = string.IsNullOrWhiteSpace(input.Author) 
                            ? manga.Author : input.Author;
            manga.Description = string.IsNullOrWhiteSpace(input.Description)
                            ? manga.Description : input.Description;
            manga.Status = input.Status;
            manga.StartDate = input.StartDate;
            manga.CompleteDate = input.CompleteDate;

            await this.mangaService.Save(manga);

            await this.publisher.Publish(new MangaUpdatedMessage() { MangaId = manga.Id, Publisher = manga.Publisher.Name });

            return Ok();
        }

        // DELETE api/<MangaController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var manga = await this.mangaService.Find(id);
            if (manga == null)
                return BadRequest();

            var success = await this.mangaService.Delete(id);

            if(success)
                await this.publisher.Publish(new MangaDeletedMessage() { MangaId = manga.Id, Publisher = manga.Publisher.Name });

            return success;
        }
    }
}
