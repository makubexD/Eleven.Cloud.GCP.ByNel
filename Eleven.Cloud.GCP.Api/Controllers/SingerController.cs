using Eleven.Cloud.GCP.Application;
using Eleven.Cloud.GCP.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SingerController : ControllerBase
    {
        private readonly ILogger<SingerController> _logger;
        private readonly ISingerApplication _singerApplication;

        public SingerController(ILogger<SingerController> logger, ISingerApplication singerApplication)
        {
            _logger = logger;
            _singerApplication = singerApplication;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<SingerDto>> GetAllSingers()
        {
            return await _singerApplication.GetAllSingers();
        }
    }
}
