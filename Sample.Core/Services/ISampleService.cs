using Sample.Core.DTO.Generic;
using Sample.Core.DTO.Sample;
using Sample.Core.DTOBase;
using Sample.Core.Entities;
using Sample.Core.Repositories.Base;
using Sample.Core.Services.Base;

namespace Sample.Core.Services
{
    public interface ISampleService : IService<IAuditableRepository<SampleTest, long>, SampleTest, long>
    {
        Task<DataTransferObject<SampleResponse>> CreateSampleAsync(RequestSampleCreate request);
        Task<DataTransferObject<List<SampleResponse>>> CreateSamplesAsync(List<RequestSampleCreate> request);
        Task<DataTransferObject<SampleResponse>> UpdateSampleAsync(RequestSampleUpdate request);
        Task<DataTransferObject<List<SampleResponse>>> GetSamplesAllAsync();
        Task<DataTransferObject<SampleResponse>> GetSampleByIdAsync(long id);
        Task<DataTransferObject<SampleResponse>> GetSampleByEmailAsync(string email);
        Task<DataTransferObject<GenericResponse>> DeleteSampleAsync(long id);
    }
}
