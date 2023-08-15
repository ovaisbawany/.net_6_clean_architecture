using AutoMapper;
using Sample.Core.DTO.Sample;
using Sample.Core.Entities;

namespace Sample.Core.Mapper
{
    public class SampleMappingProfile : Profile
    {
        public SampleMappingProfile()
        {
            CreateMap<SampleTest, SampleResponse>();
            CreateMap<RequestSampleCreate, SampleTest>();
            CreateMap<RequestSampleUpdate, SampleTest>();
        }
    }
}
