using AutoMapper;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Repositories;
using MusicPortal.Web.Util;
using System.Threading.Tasks;
using Xunit;

namespace MusicPortal.BLL.UnitTests {
    public class AlbumServiceTest {
        private IAlbumService GetAlbumService() {
            return new AlbumService(
                new UnitOfWork(new ApplicationContextBuilder().GetApplicationContext()),
                new Mapper(new MapperConfiguration(cfg => {
                    cfg.AddProfile(new AutoMapperProfile());
                }))
            );
        }

        [Theory]
        [InlineData("Radiohead" , 2, 10)]
        [InlineData("Post Malone", 1, 10)]
        public async Task GetTopArtistAlbums_PaginationTest_ReturnsLessOrEqualAmountOfItems(string artistName, int page, int itemsPerPage) {
            IAlbumService albumService = GetAlbumService();

            var albums = await albumService.GetTopArtistsAlbums(artistName, page, itemsPerPage);

            Assert.False(albums.Count > itemsPerPage, "collection size is greater than expected");
        }
    }
}
