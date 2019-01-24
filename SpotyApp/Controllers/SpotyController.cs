using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var newAlbumReleases = await _spotyService.GetNewAlbumReleases(token);

            return View();
        }
    }
}