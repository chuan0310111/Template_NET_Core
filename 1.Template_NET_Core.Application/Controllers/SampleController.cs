using _1.Template_NET_Core.Application.ViewModels;
using _2.Template_NET_Core.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _1.Template_NET_Core.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SampleController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISampleService _sampleService;

        public SampleController(IMapper mapper, ILogger<SampleController> logger, IHttpContextAccessor httpContextAccessor, ISampleService sampleService)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessor;
            this._sampleService = sampleService;
        }

        [HttpGet]
        public async Task<List<HsinChuAreaViewModel>> GetAreaAsync() 
        {

            var getAreas = await _sampleService.GetAreaAsync();
            return this._mapper.Map<List<HsinChuAreaViewModel>>(getAreas);
        }

        //[HttpGet]
        //public async Task<List<HsinChuAreaViewModel>> SetAreaAsync()
        //{
        //    var setAreas = await _sampleService.SetAreaAsync();
        //    return this._mapper.Map<List<HsinChuAreaViewModel>>(setAreas);
        //}

        // GET api/<SampleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
