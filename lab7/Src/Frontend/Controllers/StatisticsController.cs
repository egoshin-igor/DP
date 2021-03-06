using System.Threading.Tasks;
using Frontend.Clients;
using Frontend.Dtos;
using Lib.Http.Client;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    [Route("[controller]")]
    public class StatisticsController: Controller
    {
        private readonly BackendClient _backendClient;
        public StatisticsController(BackendClient backendClient)
        {
            _backendClient = backendClient;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            Response<TextStatistic> textStatisticResponse = await _backendClient.GetAsync<TextStatistic>("api/text/statistic");

            return View(textStatisticResponse.Result);
        }
    }
}