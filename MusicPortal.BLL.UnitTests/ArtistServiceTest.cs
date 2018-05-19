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
using System.Threading.Tasks;

namespace MusicPortal.BLL.UnitTests {
    public class ArtistServiceTest {
        private IArtistService GetArtistService() {
            return new ArtistService(
                new UnitOfWork(new ApplicationContextBuilder().GetApplicationContext()), 
                new Mapper(new MapperConfiguration(cfg => {
                    cfg.AddProfile(new AutoMapperProfile());
                }))
            );
        }

        [Theory]
        [InlineData(1, 40)]
        [InlineData(5, 20)]
        [InlineData(6, 44)]
        public async Task GetTopArtists_PaginationTest_ReturnsLessOrEqualAmountOfItems(int page, int itemsPerPage) {
            IArtistService artistService = GetArtistService();

            var artists = await artistService.GetTopArtists(page, itemsPerPage);

            Assert.False(artists.Count > itemsPerPage, "collection size is greater than expected");
        }

        [Theory]
        [InlineData("Bones")]
        [InlineData("Lil Peep")]
        [InlineData("Surrenderdorothy")]
        public async Task GetSimilarArtists_ResponseTest_ReturnsNotEmptyCollection(string name) {
            IArtistService artistService = GetArtistService();

            var artists = await artistService.GetSimilarArtists(name);

            Assert.NotEmpty(artists);
        }
    }
}
