using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotyApp.Models;
using SpotyApp.Services.Interfaces;

namespace SpotyApp.Controllers
{
    public class SpotyController : Controller
    {
        private readonly ISpotyService _spotyService;

        public SpotyController(ISpotyService service)
        {
            _spotyService = service;
        }


        public async Task<IActionResult> Index()
        {
            var token = await _spotyService.GetToken();


            /*
             * Get Data Syncronous
             * Details:
             * 1)We are sending a request to Spotify Api and we retrieve new releases for Argentina. We're WAITING for the response.
             * 2)We are sending a request to Spotify Api and we retrieve new releases for Australia. We're WAITING for the response.
             * 3)We are sending a request to Spotify Api and we retrieve new releases for NewZealand. We're WAITING for the response.
             * 4)We are sending a request to Spotify Api and we retrieve new releases for Usa. We're WAITING for the response.
             * 
             * Then, if each request delay 10 seconds, we'd have a total: 40 seconds.
             */

            Stopwatch timerSync = Stopwatch.StartNew();
            var newReleasesArgentina = await _spotyService.GetNewAlbumReleases(token, Countries.ARGENTINA, 50);
            var newReleasesAustralia = await _spotyService.GetNewAlbumReleases(token, Countries.AUSTRALIA, 50);
            var newReleasesNewZealand = await _spotyService.GetNewAlbumReleases(token, Countries.NEW_ZEALAND, 50);
            var newReleasesUsa = await _spotyService.GetNewAlbumReleases(token, Countries.USA, 50);
            timerSync.Stop();

            /*
             * Get Data Asyncronous
             * 
             * Details:
             * For each country, We are sending a request to Spotify Api, generate a Promise. The process is started, but it's NOT BLOCKING, the execution continue
             * 
             * 1) The request start for retrieve Argentina Data and continue.
             * 2) The request start for retrieve Australia Data and continue.
             * 3) The request start for retrieve NewZealand Data and continue.
             * 4) The request start for retrieve Usa Data and continue.
             * 
             * 5) In this point, we have 4 request in PARALLEL --> 4 tasks are executing at same time. --> CONCURRENCY!
             * 
             * 6) When the code meets a "await" STOP the executing until retrieve Argentina Data.
             *    In this moment the process Index() is blocked and the thread is FREE until we have a API response --> EXPLOIT SERVER RESOURCES!
             * 
             * 7) When we have a APi Response, came back to the line:
             * var newReleasesArgentinaResult = await newReleasesArgentinaPromise;
             * And continue!
             * 
             * 8) It's the same for each await Promise.
             * 
             */

            Stopwatch timerAsync = Stopwatch.StartNew();
            var newReleasesArgentinaPromise =  _spotyService.GetNewAlbumReleases(token, Countries.ARGENTINA, 50);
            var newReleasesAustraliaPromise =  _spotyService.GetNewAlbumReleases(token, Countries.AUSTRALIA, 50);
            var newReleasesNewZealandPromise =  _spotyService.GetNewAlbumReleases(token, Countries.NEW_ZEALAND, 50);
            var newReleasesUsaPromise =  _spotyService.GetNewAlbumReleases(token, Countries.USA, 50);

            var newReleasesArgentinaResult = await newReleasesArgentinaPromise;
            var newReleasesAustraliaResult = await newReleasesAustraliaPromise;
            var newReleasesNewZealandResult = await newReleasesNewZealandPromise;
            var newReleasesUsaResult = await newReleasesUsaPromise;

            timerAsync.Stop();


            return View();
        }
    }
}