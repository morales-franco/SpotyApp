using Microsoft.Extensions.Configuration;
using SpotyApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotyApp.Services
{
    public class SpotyService : ISpotyService
    {
        private readonly IConfiguration _configuration;

        public SpotyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken()
        {
            HttpClient httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("https://api.github.com/");
            //// GitHub API versioning
            //httpClient.DefaultRequestHeaders.Add("Accept",
            //    "application/vnd.github.v3+json");
            //// GitHub requires a user-agent
            //httpClient.DefaultRequestHeaders.Add("User-Agent",
            //    "HttpClientFactory-Sample");

            //var response = httpClient.GetAsync(
            //"/repos/aspnet/docs/issues?state=open&sort=created&direction=desc").Result;

            //response.EnsureSuccessStatusCode();

    

            var clientId = _configuration["Spotify:ClientId"];
            var clientSecret = _configuration["Spotify:ClientSecret"];

            httpClient.BaseAddress = new Uri("https://accounts.spotify.com/api/");
            var response = httpClient.GetAsync("/token").Result;

            return clientId;
        }
    }
}
