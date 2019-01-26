using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotyApp.Services.Interfaces;
using SpotyApp.ViewModels;

namespace SpotyApp.Controllers
{
    public class RadioController : Controller
    {
        private readonly IRadioService _service;

        public RadioController(IRadioService service)
        {
            _service = service;
        }

        /*
         * Synchronous Method
         */
        public IActionResult IndexSync()
        {
            var model = new RadioVM();
            var timer = Stopwatch.StartNew();

            model.Albums = _service.GetTopAlbums(); //Delay 2000ms
            model.Artists = _service.GetTopArtists(); //Delay 2000ms
            model.CurrentNumberOfListeners = _service.GetCurrentNumberOfListeners(); //Delay 1000ms

            timer.Stop();

            ViewBag.Delay = timer.ElapsedMilliseconds; //Sync total delay = 2000ms + 2000ms + 1000ms = 5000ms

            return View(model);
        }


        /*
         * Asynchronous Method but execute syncronous
         */
        public async Task<IActionResult> IndexAsyncA()
        {
            var model = new RadioVM();
            var timer = Stopwatch.StartNew();

            model.Albums = await _service.GetTopAlbumsAsync(); //Delay 2000ms
            model.Artists = await _service.GetTopArtistsAsync(); //Delay 2000ms
            model.CurrentNumberOfListeners = await _service.GetCurrentNumberOfListenersAsync(); //Delay 1000ms

            timer.Stop();

            ViewBag.Delay = timer.ElapsedMilliseconds; //Sync total delay = 2000ms + 2000ms + 1000ms = 5000ms

            return View(model);
        }

        /*
         * Asynchronous Method
         */
        public async Task<IActionResult> IndexAsyncB()
        {
            var model = new RadioVM();
            var timer = Stopwatch.StartNew();

            //Task&async power!
            var albumsPromise =  _service.GetTopAlbumsAsync(); //Delay 2000ms
            var artistPromise = _service.GetTopArtistsAsync(); //Delay 2000ms
            var numberOfListenersPromise = _service.GetCurrentNumberOfListenersAsync(); //Delay 1000ms

            //Get results
            model.Albums = await albumsPromise;
            model.Artists = await artistPromise;
            model.CurrentNumberOfListeners = await numberOfListenersPromise;

            timer.Stop();

            ViewBag.Delay = timer.ElapsedMilliseconds;

            return View(model);
        }
    }
}