using _1.Template_NET_Core.Application.Controllers.Validators;
using _1.Template_NET_Core.Application.Parameters;
using _1.Template_NET_Core.Application.ViewModels;
using _2.Template_NET_Core.Services.Interface;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _1.Template_NET_Core.Application.Controllers
{
    /// <summary>
    /// 範例API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        /// <summary>
        /// 取得鄉鎮市公所名稱 By Cache
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAreaAsync")]
        public async Task<List<HsinChuAreaViewModel>> GetAreaAsync([FromQuery] HsinChuAreaParameter parameter, [FromServices] IValidator<HsinChuAreaParameter> validator) 
        {
            var logName = $"[{this._httpContextAccessor?.HttpContext?.TraceIdentifier}] [Template_NET_Core] [SampleController] [GetAreaAsync()] [channel:{parameter.Channel}] [取得鄉鎮市公所名稱 By Cache]";

            try
            {
                this._logger.LogInformation($"{logName}  RQ");

                var res = validator.Validate(parameter);
                if (res.IsValid)
                {
                    var getAreas = await _sampleService.GetAreaAsync();
                    return this._mapper.Map<List<HsinChuAreaViewModel>>(getAreas);
                }
                else
                {
                    throw new ValidationException($"{string.Join("、", res.Errors.Select(x => x.ErrorMessage))}");

                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }


        }

        /// <summary>
        /// 強制取得鄉鎮市公所名稱 重設cache
        /// </summary>
        /// <returns></returns>
        [HttpPost("SetAreaAsync")]
        public async Task<List<HsinChuAreaViewModel>> SetAreaAsync([FromBody] HsinChuAreaParameter parameter, [FromServices] IValidator<HsinChuAreaParameter> validator)
        {
            var logName = $"[{this._httpContextAccessor?.HttpContext?.TraceIdentifier}] [Template_NET_Core] [SampleController] [SetAreaAsync()] [channel:{parameter.Channel}] [強制取得鄉鎮市公所名稱 重設cache]";

            try
            {
                this._logger.LogInformation($"{logName}  RQ");

                var res = validator.Validate(parameter);
                if (res.IsValid)
                {
                    var getAreas = await _sampleService.GetAreaAsync();
                    return this._mapper.Map<List<HsinChuAreaViewModel>>(getAreas);
                }
                else
                {
                    throw new ValidationException($"{string.Join("、", res.Errors.Select(x => x.ErrorMessage))}");

                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }


        }


    }
}
