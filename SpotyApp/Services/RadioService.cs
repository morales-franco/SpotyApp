using SpotyApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpotyApp.Services
{
    public class RadioService : IRadioService
    {
        #region Sync
        public int GetCurrentNumberOfListeners()
        {
            Thread.Sleep(1000);
            return 1000;
        }

        public IList<string> GetTopAlbums()
        {
            Thread.Sleep(2000);
            return GetHarcodedList("Album");
        }


        public IList<string> GetTopArtists()
        {
            Thread.Sleep(2000);
            return GetHarcodedList("Artist");
        }

        #endregion

        #region Async
        public async Task<int> GetCurrentNumberOfListenersAsync()
        {
            await Task.Delay(1000);
            return 1000;
        }

        public async Task<IList<string>> GetTopAlbumsAsync()
        {
            await Task.Delay(2000);
            return GetHarcodedList("Album");
        }


        public async Task<IList<string>> GetTopArtistsAsync()
        {
            await Task.Delay(2000);
            return GetHarcodedList("Artist");
        }

        #endregion

        private IList<string> GetHarcodedList(string entityName)
        {
            var list = new List<string>();

            for (int i = 1; i <= 20; i++)
            {
                list.Add($"{ entityName }_{ i }");
            }
            return list;
        }
    }
}
