using MediatR;
using Sample.Core.DTO;
using Sample.Core.DTOBase;
using Sample.Core.Queries;
using Sample.Core.Services;

namespace Sample.Application.Handlers.QueryHandlers
{
    // Get all sample query handler with List<Sample> response as output
    public class SampleQueryHandler : IRequestHandler<GetAllSampleQuery, DataTransferObject<List<SampleResponse>>>,
                                        IRequestHandler<GetSampleByIdQuery, DataTransferObject<SampleResponse>>,
                                        IRequestHandler<GetSampleByEmailQuery, DataTransferObject<SampleResponse>>
    {
        private readonly ISampleService _sampleService;

        public SampleQueryHandler(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }
        public async Task<DataTransferObject<List<SampleResponse>>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.GetSamplesAllAsync();
            return result;
        }

        public async Task<DataTransferObject<SampleResponse>> Handle(GetSampleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.GetSampleByIdAsync(request.Id);
            return result;
        }

        public async Task<DataTransferObject<SampleResponse>> Handle(GetSampleByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.GetSampleByEmailAsync(request.Email);
            return result;
        }
    }
}
