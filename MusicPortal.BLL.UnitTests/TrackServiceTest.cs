using AutoMapper;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Repositories;
using MusicPortal.Web.Util;
using System.Threading.Tasks;
using Xunit;

namespace MusicPortal.BLL.UnitTests {
    public class TrackServiceTest {
        private ITrackService GetTrackService() {
            return new TrackService(
                new UnitOfWork(new ApplicationContextBuilder().GetApplicationContext()),
                new Mapper(new MapperConfiguration(cfg => {
                    cfg.AddProfile(new AutoMapperProfile());
                }))
            );
        }

        [Theory]
        [InlineData(3, 40)]
        [InlineData(10, 50)]
        public async Task GetTopTracks_PaginationTest_ReturnsLessOrEqualAmountOfItems(int page, int itemsPerPage) {
            ITrackService trackService = GetTrackService();

            var tracks = await trackService.GetTopTracks(page, itemsPerPage);

            Assert.False(tracks.Count > itemsPerPage, "collection size is greater than expected");
        }

        [Theory]
        [InlineData("Bones", 1, 10)]
        [InlineData("Post Malone", 3, 20)]
        public async Task GetTopArtistsTracks_PaginationTest_ReturnsLessOrEqualAmountOfItems(string artistName, int page, int itemsPerPage) {
            ITrackService trackService = GetTrackService();

            var tracks = await trackService.GetTopArtistsTracks(artistName, page, itemsPerPage);
            
            Assert.False(tracks.Count > itemsPerPage, "collection size is greater than expected");
        }

        [Theory]
        [InlineData("To Pimp a Butterfly")]
        [InlineData("DAMN.")]
        [InlineData("Swimming Pools (Drank)")]
        public void GetAlbumTracks_ResponseTest_ReturnsNotEmptyCollection(string albumName) {
            ITrackService trackService = GetTrackService();

            var tracks = trackService.GetAlbumTracks(albumName);

            Assert.NotEmpty(tracks);
        }
    }
}
