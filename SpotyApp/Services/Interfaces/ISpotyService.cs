using SpotyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotyApp.Services.Interfaces
{
    public interface ISpotyService
    {
        Task<Token> GetToken();

        Task<NewAlbumReleases> GetNewAlbumReleases(Token token, string country = "", int limit = 20, int offset = 0);


    }
}
