using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using Frontend;
using System.Net.Http;
using System.Net.Http.Headers;
using Frontend.Clients;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        BackendClient _backendClient;

        public HomeController(BackendClient backendClient)
        {
            _backendClient = backendClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("upload")]
        public IActionResult Upload()
        {
            return View();
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadAsync(string data)
        {
            return Ok(await _backendClient.PostAsync<string, string>(data, "api/values"));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
