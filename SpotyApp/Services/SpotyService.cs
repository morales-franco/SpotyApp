using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotyApp.Models;
using SpotyApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotyApp.Services
{
    public class SpotyService : ISpotyService
    {
        private readonly IConfiguration _configuration;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotyService(IConfiguration configuration)
        {
            _configuration = configuration;
            _clientId = _configuration["Spotify:ClientId"];
            _clientSecret = _configuration["Spotify:ClientSecret"];

        }

        public async Task<NewAlbumReleases> GetNewAlbumReleases(Token token, string country = "", int limit = 20, int offset = 0)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.AccessToken }");
                var response = await client.GetAsync(GetUrlApiNewAlbumReleases(country, limit,offset));
                response.EnsureSuccessStatusCode();

                string msg = await response.Content.ReadAsStringAsync();
                var newReleases = JsonConvert.DeserializeObject<NewAlbumReleases>(msg);
                return newReleases;
            }
        }

        public string GetUrlApiNewAlbumReleases(string country = "", int limit = 20, int offset = 0)
        {
            limit = Math.Min(limit, 50);
            StringBuilder builder = new StringBuilder("https://api.spotify.com/v1/browse/new-releases");
            builder.Append("?limit=" + limit);
            builder.Append("&offset=" + offset);
            if (!string.IsNullOrEmpty(country))
                builder.Append("&country=" + country);
            return builder.ToString();
        }

        public async Task<Token> GetToken()
        {
            var authHeader = GetAuthHeader();

            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            };

            HttpContent content = new FormUrlEncodedContent(args);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", authHeader);
                var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);
                response.EnsureSuccessStatusCode();

                string msg = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(msg);
                return token;
            }
        }

        private string GetAuthHeader() => $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret))}";
    }
}
