using Blazor_Radzen_Data_Example.Client.Services;
using Blazor_Radzen_Data_Example.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System.IO.Pipes;

namespace Blazor_Radzen_Data_Example.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempratureController : ControllerBase
    {

        private readonly ITempratureService _TempratureService;
        private readonly ILogger<TempratureController> _Logger;

        public TempratureController(ITempratureService tempratureService, ILogger<TempratureController> logger)
        {
            _TempratureService = tempratureService;
            _Logger = logger;
        }

        [HttpGet]
        [Route("range")]
        public async Task<List<TempraureInfo>> GetAllTempraturesAsync(DateTime start,DateTime end)
        {
            return await _TempratureService.GetTempraturesAsync(start,end);
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentTempratureAsync()
        {
            var temp = await _TempratureService.GetLatestTempratureAsync();

            if (temp == null)
            {
                return NotFound();
            }

            return Ok(temp);
        }

    }
}
