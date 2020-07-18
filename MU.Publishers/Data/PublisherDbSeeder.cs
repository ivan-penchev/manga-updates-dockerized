using Microsoft.EntityFrameworkCore;
using MU.Common.Services;
using MU.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Publishers.Data
{
    public class PublisherDbSeeder : IDataSeeder
    {
        private readonly PublishersDbContext publishersDbContext;

        public PublisherDbSeeder(PublishersDbContext publishersDbContext)
        {
            this.publishersDbContext = publishersDbContext;
        }

        public void SeedData()
        {
            if(publishersDbContext.Genres.Any() 
                || publishersDbContext.MangaPublishers.Any())
            { 
                return; 
            }
            Task
                .Run(async () =>
                {

                    var allGenres = this.GetGenres();
                    this.publishersDbContext.Genres.AddRange(allGenres);
                    await this.publishersDbContext.SaveChangesAsync();
                    var allPublishers = this.GetMangaPublishers();
                    this.publishersDbContext.MangaPublishers.AddRange(allPublishers);
                    await this.publishersDbContext.SaveChangesAsync();

                    this.publishersDbContext.Mangas.AddRange(this.GetMangas(allPublishers, allGenres));
                    await this.publishersDbContext.SaveChangesAsync();

                })
                .GetAwaiter()
                .GetResult();
        }
        //Action  Adventure  Comedy  Drama  Fantasy  Shounen  Supernatural 
        private List<Genre> GetGenres()
        {
            return new List<Genre>
            {
                new Genre{Name="Action"},
                new Genre{Name="Adventure"},
                new Genre{Name="Comedy"},
                new Genre{Name="Fantasy"},
                new Genre{Name="Shounen", Description = @"is manga aimed at a young teen male target-demographic. 
The age group varies with individual readers and different magazines, 
but it is primarily intended for boys between the ages of 12 and 18."},
                new Genre{Name="Supernatural"},
            };
        }

        private List<MangaPublisher> GetMangaPublishers()
        {
            return new List<MangaPublisher> { 
                new MangaPublisher{Name="Shueisha", SiteUrl="https://www.shueisha.co.jp/"},
                new MangaPublisher{Name="Hakusensha", SiteUrl="http://www.hakusensha.co.jp/"},
                new MangaPublisher{Name="Kodansha", SiteUrl="http://www.kodansha.co.jp/"},
                new MangaPublisher{Name="Square Enix", SiteUrl="https://www.jp.square-enix.com/"},
                new MangaPublisher{Name="Daiwon", SiteUrl="http://daewonmedia.com/"},
            };
        }

        private List<Manga> GetMangas(List<MangaPublisher> publishers, List<Genre> genres)
        {
            return new List<Manga>
            {
                new Manga{
                    Author = "Eichiro Oda",
                    Title = "One Piece",
                    Publisher = publishers.Where(x=>x.Name=="Shueisha").First(),
                    Genre = genres.Where(x=>x.Name=="Shounen").First(),
                    Status = PublishingStatus.Ongoing,
                    StartDate = new DateTime(1997,7,22),
                    Description = @"Before the Pirate King was executed, he dared the many pirates of the world to seek out the fortune that he left behind in one piece.
As a child, Monkey D. Luffy dreamed of becoming the King of the Pirates. 
But his life changed when he accidentally gained the power to stretch like rubber...
at the cost of never being able to swim again! Now Luffy, with the help of a motley collection of nakama,
is setting off in search of One Piece, said to be the greatest treasure in the world."
                },
                new Manga{
                    Author = "Hiro Mashima",
                    Title = "Fairy Tail",
                    Publisher = publishers.Where(x=>x.Name=="Kodansha").First(),
                    Genre = genres.Where(x=>x.Name=="Action").First(),
                    Status = PublishingStatus.Completed,
                    StartDate = new DateTime(2006,8,2),
                    CompleteDate = new DateTime(2017,7,26),
                    Description = @"Celestial wizard Lucy wants to join the Fairy Tail, a guild for the most powerful wizards. 
But instead, her ambitions land her in the clutches of a gang of unsavory pirates led by a devious magician. 
Her only hope is Natsu, a strange boy she happens to meet on her travels. 
Natsu's not your typical hero - but he just might be Lucy's best hope."
                },
                new Manga{
                    Author = "Yoshihiro Togashi",
                    Title = "Hunter Hunter",
                    Publisher = publishers.Where(x=>x.Name=="Shueisha").First(),
                    Genre = genres.Where(x=>x.Name=="Supernatural").First(),
                    Status = PublishingStatus.Hiatus,
                    StartDate = new DateTime(1998,3,16),
                    Description = @"Hunters are a special breed, dedicated to tracking down treasures, magical beasts, and even other men.
But such pursuits require a license, and less than one in a hundred thousand can pass the grueling qualification exam.
Those who do pass gain access to restricted areas, amazing stores of information, and the right to call themselves Hunters."
                },
            };
        }
    }
}
