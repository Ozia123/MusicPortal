using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.UnitTests.Factories;
using System.Threading.Tasks;
using Xunit;

namespace MusicPortal.BLL.UnitTests {
    public class ArtistServiceTest {
        [Theory]
        [InlineData(1, 40)]
        [InlineData(5, 20)]
        [InlineData(6, 44)]
        public async Task GetTopArtists_PaginationTest_ReturnsLessOrEqualAmountOfItems(int page, int itemsPerPage) {
            IArtistService artistService = ServiceFactory.GetArtistService();

            var artists = await artistService.GetTopArtists(page, itemsPerPage);

            Assert.False(artists.Count > itemsPerPage, "collection size is greater than expected");
        }

        [Theory]
        [InlineData("Bones")]
        [InlineData("Lil Peep")]
        [InlineData("Surrenderdorothy")]
        public async Task GetSimilarArtists_ResponseTest_ReturnsNotEmptyCollection(string name) {
            IArtistService artistService = ServiceFactory.GetArtistService();

            var artists = await artistService.GetSimilarArtists(name);

            Assert.NotEmpty(artists);
        }
    }
}
