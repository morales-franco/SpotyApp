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
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotyService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://accounts.spotify.com/api/");
            _clientId = _configuration["Spotify:ClientId"];
            _clientSecret = _configuration["Spotify:ClientSecret"];

        }

        public string GetToken()
        {
            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            };

            HttpContent content = new FormUrlEncodedContent(args);
            _httpClient.DefaultRequestHeaders.Add("Authorization", GetAuthHeader());
            var response = _httpClient.PostAsync("token", content).Result;

            response.EnsureSuccessStatusCode();

            string msg = response.Content.ReadAsStringAsync().Result;

            var token = JsonConvert.DeserializeObject<Token>(msg);

            //####################################################
            //Consumir recursos

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token.AccessToken }");
                //HttpResponseMessage response1 = httpClient.GetAsync("https://api.spotify.com/v1/tracks/11dFghVXANMlKmJXsNCbNl").Result;
                HttpResponseMessage response1 = httpClient.GetAsync("https://api.spotify.com/v1/me").Result;
                
                string responseBody = response1.Content.ReadAsStringAsync().Result;
            }


            return _clientId;
        }

        private string GetAuthHeader() => $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret))}";
    }
}
