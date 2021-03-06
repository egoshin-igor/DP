﻿using System;
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
    [Route("")]
    [Route("[controller]")]
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
            Response<TextProcessingResultDto> response = await _backendClient
                .GetAsync<TextProcessingResultDto>($"api/text/text-processing-result?textId={textId}");
            if (!response.IsSuccessStatusCode)
            {
                return Error();
            }

            TextProcessingResultDto textProcessingResultDto = response.Result;

            return View("TextDetails", textProcessingResultDto);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadAsync(string data, RegionType regionType)
        {
            UploadDto uploadDto = new UploadDto {Text = data, RegionType = regionType};
            string textId = (await _backendClient.PostAsync<string, UploadDto>(uploadDto, "api/text")).Result;
            return RedirectToAction("GetTextDetailsAsync", new { textId = textId });
        }

        [HttpGet("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
