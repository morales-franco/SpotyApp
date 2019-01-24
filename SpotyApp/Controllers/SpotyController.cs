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
        public IActionResult Index()
        {
            var token = _spotyService.GetToken();

            return View();
        }
    }
}