using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.API.Controllers.Base;
using Sample.Core.DTO.Generic;
using Sample.Core.DTO.Sample;
using Sample.Core.DTOBase;
using Sample.Core.Services;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : BaseController
    {
        private readonly ISampleService _sampleService;
        public SampleController
            (IMapper mapper
            , ISampleService sampleService)
            : base(mapper)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _sampleService.GetSamplesAllAsync();
            return JsonResponse<DataTransferObject<List<SampleResponse>>>(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _sampleService.GetSampleByIdAsync(id);
            return JsonResponse<DataTransferObject<SampleResponse>>(result);
        }

        [HttpGet("email")]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _sampleService.GetSampleByEmailAsync(email);
            return JsonResponse<DataTransferObject<SampleResponse>>(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSample([FromBody] RequestSampleCreate requestSampleCreate)
        {
            var result = await _sampleService.CreateSampleAsync(requestSampleCreate);
            return JsonResponse<DataTransferObject<SampleResponse>>(result);
        }


        [HttpPost("samples")]
        [ProducesResponseType(typeof(DataTransferObject<List<SampleResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DataTransferObject<List<SampleResponse>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSamples([FromBody] List<RequestSampleCreate> request)
        {
            var result = await _sampleService.CreateSamplesAsync(request);
            return JsonResponse<DataTransferObject<List<SampleResponse>>>(result);
        }

        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DataTransferObject<SampleResponse>), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSample(int id, [FromBody] RequestSampleUpdate request)
        {
            request.Id = id;
            var result = await _sampleService.UpdateSampleAsync(request);
            return JsonResponse<DataTransferObject<SampleResponse>>(result);
        }

        [ProducesResponseType(typeof(DataTransferObject<GenericResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DataTransferObject<GenericResponse>), StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteSample/{id}")]
        public async Task<IActionResult> DeleteSample(int id)
        {
            var result = await _sampleService.DeleteSampleAsync(id);
            return JsonResponse<DataTransferObject<GenericResponse>>(result);
        }

    }
}
