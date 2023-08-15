using MediatR;
using Sample.Core.Commands;
using Sample.Core.DTO;
using Sample.Core.DTO.Generic;
using Sample.Core.DTOBase;
using Sample.Core.Services;

namespace Sample.Application.Handlers.CommandHandler
{
    // Sample create command handler with SampleResponse as output
    public class SampleCommandHandler : IRequestHandler<CreateSampleCommand, DataTransferObject<SampleResponse>>,
                                        IRequestHandler<UpdateSampleCommand, DataTransferObject<SampleResponse>>,
                                        IRequestHandler<DeleteSampleCommand, DataTransferObject<GenericResponse>>,
                                        IRequestHandler<CreateSamplesCommand, DataTransferObject<List<SampleResponse>>>
    {
        private readonly ISampleService _sampleService;
        public SampleCommandHandler(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }
        public async Task<DataTransferObject<SampleResponse>> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.CreateSampleAsync(request);
            return result;
        }

        public async Task<DataTransferObject<List<SampleResponse>>> Handle(CreateSamplesCommand request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.CreateSamplesAsync(request.Samples);
            return result;
        }

        public async Task<DataTransferObject<SampleResponse>> Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.UpdateSampleAsync(request);
            return result;
        }

        public async Task<DataTransferObject<GenericResponse>> Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
        {
            var result = await _sampleService.DeleteSampleAsync(request.Id);
            return result;
        }
    }
}
