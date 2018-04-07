using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicPortal.BLL.BusinessModels {
    public class LastFmApi {
        private readonly HttpClient client;
        private readonly string apiKey;

        public LastFmApi(string apiKey) {
            this.apiKey = apiKey;
            client = new HttpClient();
            client.BaseAddress = new Uri(@"http://ws.audioscrobbler.com/2.0/");
        }

        private string GetTrackInfoUrl(string artistName, string trackName) {
            return "?method=track.getInfo&api_key="
                + apiKey
                + "&artist="
                + artistName
                + "&track="
                + trackName
                + "&format=json";
        }

        private async Task<string> HttpGetAsync(string url) {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private string ParseTitle(string json) {
            JObject parsed = JObject.Parse(json);
            var parsedToken = parsed.SelectToken("track.album.title");
            if (parsedToken == null) {
                return null;
            }
            string value = parsedToken.Value<string>();
            return value;
        }

        public async Task<string> GetAlbumNameByTrackName(string artistName, string trackName) {
            string url = GetTrackInfoUrl(artistName, trackName);
            string responseJson = await HttpGetAsync(url);
            return ParseTitle(responseJson);
        }
    }
}
