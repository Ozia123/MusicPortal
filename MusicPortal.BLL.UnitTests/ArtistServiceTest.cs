using MusicPortal.BLL.Services;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.EF;
using AutoMapper;
using Xunit;
using MusicPortal.Web.Util;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.BLL.UnitTests {
    public class ArtistServiceTest {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Database=MusicPortalDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        private IArtistService GetArtistService() {
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("MusicPortal.DAL"))
                .Options;
            return new ArtistService(
                new UnitOfWork(new ApplicationContext(dbOptions)), 
                new Mapper(new MapperConfiguration(cfg => {
                    cfg.AddProfile(new AutoMapperProfile());
                }))
            );
        }

        [Theory]
        [InlineData(1, 40)]
        [InlineData(5, 20)]
        [InlineData(6, 44)]
        public async void GetTopArtists_PaginationTest_ReturnsLessOrEqualAmountOfItems(int page, int itemsPerPage) {
            IArtistService artistService = GetArtistService();

            List<ArtistDto> artists = await artistService.GetTopArtists(page, itemsPerPage);

            Assert.False(artists.Count > 44, "collection size is greater than expected");
        }

        [Theory]
        [InlineData("Bones")]
        [InlineData("Lil Peep")]
        [InlineData("Surrenderdorothy")]
        public async void GetSimilarArtists_ResponseTest_ReturnsNotEmptyCollection(string name) {
            IArtistService artistService = GetArtistService();

            List<ArtistDto> artists = await artistService.GetSimilarArtists(name);

            Assert.NotEmpty(artists);
        }
    }
}
