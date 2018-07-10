using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.UnitTests.Factories;
using System.Threading.Tasks;
using Xunit;

namespace MusicPortal.BLL.UnitTests {
    public class AlbumServiceTest {
        [Theory]
        [InlineData("Radiohead" , 2, 10)]
        [InlineData("Post Malone", 1, 10)]
        public async Task GetTopArtistAlbums_PaginationTest_ReturnsLessOrEqualAmountOfItems(string artistName, int page, int itemsPerPage) {
            IAlbumService albumService = ServiceFactory.GetAlbumService();

            var albums = await albumService.GetTopArtistsAlbums(artistName, page, itemsPerPage);

            Assert.False(albums.Count > itemsPerPage, "collection size is greater than expected");
        }
    }
}
