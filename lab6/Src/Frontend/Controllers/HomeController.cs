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
using Lib.Http.Client;
using Frontend.Types;
using Frontend.Dtos;

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

        [Route("text-details")]
        [HttpGet]
        public async Task<IActionResult> GetTextDetailsAsync(string textId)
        {
            Response<double?> response = await _backendClient.GetAsync<double?>($"api/text/rank?textId={textId}");
            if (!response.IsSuccessStatusCode)
            {
                return Error();
            }

            double? rank = response.Result;
            string rankString = rank != null ? Math.Round(rank.Value, 2).ToString() : null;
            if (rankString == null)
            {
                return NotFound();
            }

            return View("TextDetails", rankString);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadAsync(string data, RegionType regionType)
        {
            UploadDto uploadDto = new UploadDto {Text = data, RegionType = regionType};
            string textId = (await _backendClient.PostAsync<string, UploadDto>(uploadDto, "api/text")).Result;
            return RedirectToAction("GetTextDetailsAsync", new { textId = textId });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
