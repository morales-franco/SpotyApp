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

            model.Albums = _service.GetTopAlbums();
            model.Artists = _service.GetTopArtists();
            model.CurrentNumberOfListeners = _service.GetCurrentNumberOfListeners();

            timer.Stop();

            ViewBag.Delay = timer.ElapsedMilliseconds;

            return View(model);
        }


        /*
         * Asynchronous Method but execute syncronous
         */
        public IActionResult IndexAsyncA()
        {
            return View();
        }

        /*
         * Asynchronous Method
         */
        public IActionResult IndexAsyncB()
        {
            return View();
        }
    }
}