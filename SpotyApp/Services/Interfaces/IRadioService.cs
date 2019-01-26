using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotyApp.Services.Interfaces
{
    public interface IRadioService
    {
        IList<string> GetTopArtists();
        IList<string> GetTopAlbums();
        int GetCurrentNumberOfListeners();


        Task<IList<string>> GetTopAlbumsAsync();
        Task<IList<string>> GetTopArtistsAsync();
        Task<int> GetCurrentNumberOfListenersAsync();
    }
}
