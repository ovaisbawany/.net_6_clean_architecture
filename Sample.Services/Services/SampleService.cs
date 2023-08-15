using AutoMapper;
using Microsoft.Extensions.Logging;
using Sample.Core.DTO.Generic;
using Sample.Core.DTO.Sample;
using Sample.Core.DTOBase;
using Sample.Core.Entities;
using Sample.Core.Generic;
using Sample.Core.Helpers;
using Sample.Core.Repositories;
using Sample.Core.Repositories.Base;
using Sample.Core.Services;
using Sample.Service.Services.Base;

namespace Sample.Service.Services
{
    public class SampleService : Service<IAuditableRepository<SampleTest, long>, SampleTest, long>, ISampleService
    {
        private readonly IAuditColumns<SampleTestChild, long> _auditColumns;
        private readonly ILogger<SampleService> _logger;
        public SampleService(
            IUnitOfWork unitOfWork
            , IAuditableRepository<SampleTest, long> repository
            , IMapper mapper
            , ILogger<SampleService> logger
            , IAuditColumns<SampleTestChild, long> auditColumns
            ) : base(unitOfWork, repository, mapper)
        {
            _auditColumns = auditColumns;
            _logger = logger;
        }

        public async Task<DataTransferObject<SampleResponse>> CreateSampleAsync(RequestSampleCreate request)
        {
            var response = new DataTransferObject<SampleResponse>();
            var sampleEntity = _mapper.Map<SampleTest>(request);
            _auditColumns.SetAuditColumns(sampleEntity.SampleTestChild.ToList());
            var result = await base.CreateAsync(sampleEntity);

            await SaveContext();
            var sampleResponse = _mapper.Map<SampleResponse>(result);
            response.Result = sampleResponse;
            return response;
        }

        public async Task<DataTransferObject<List<SampleResponse>>> CreateSamplesAsync(List<RequestSampleCreate> request)
        {
            var response = new DataTransferObject<List<SampleResponse>>();
            var sampleEntities = new List<SampleTest>();
            foreach (var item in request)
            {
                sampleEntities.Add(_mapper.Map<SampleTest>(item));
            }
            var result = await base.CreateAsync(sampleEntities);

            var sampleResponses = new List<SampleResponse>();
            foreach (var item in result)
            {
                sampleResponses.Add(_mapper.Map<SampleResponse>(item));
            }

            response.Result = sampleResponses;
            return response;
        }

        public async Task<DataTransferObject<SampleResponse>> UpdateSampleAsync(RequestSampleUpdate request)
        {
            var response = new DataTransferObject<SampleResponse>();
            var sampleEntity = await GetAsync(request.Id);
            if (sampleEntity == null)
            {
                _logger.LogInformation("Not Found");
                return ErrorResponseHelper.CreateErrorResponse<SampleResponse>(new List<string>
                {
                    "Not Found"
                });
            }
            _mapper.Map(request, sampleEntity);
            var result = await base.UpdateAsync(sampleEntity);
            await SaveContext();
            var sampleResponse = _mapper.Map<SampleResponse>(result);
            response.Result = sampleResponse;
            return response;
        }

        public async Task<DataTransferObject<GenericResponse>> DeleteSampleAsync(long id)
        {
            await base.DeleteAsync(id);
            await SaveContext();
            return new DataTransferObject<GenericResponse>(new GenericResponse() { Id = id, Message = "" });
        }

        public async Task<DataTransferObject<List<SampleResponse>>> GetSamplesAllAsync()
        {
            var response = new DataTransferObject<List<SampleResponse>>();
            var samples = await base.GetAllAsync();
            var result = _mapper.Map<List<SampleResponse>>(samples);
            response.Result = result;
            return response;
        }

        public async Task<DataTransferObject<SampleResponse>> GetSampleByIdAsync(long id)
        {
            var response = new DataTransferObject<SampleResponse>();
            var sample = await base.GetAsync(id);
            var result = _mapper.Map<SampleResponse>(sample);
            response.Result = result;
            return response;
        }

        public async Task<DataTransferObject<SampleResponse>> GetSampleByEmailAsync(string email)
        {
            var response = new DataTransferObject<SampleResponse>();
            var sample = await Repository.Find(x => x.Email == email);
            var result = _mapper.Map<SampleResponse>(sample);
            response.Result = result;
            return response;
        }
    }
}
