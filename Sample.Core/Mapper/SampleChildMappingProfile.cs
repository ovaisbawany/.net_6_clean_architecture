using AutoMapper;
using Sample.Core.DTO.Sample;
using Sample.Core.DTO.SampleChild;
using Sample.Core.Entities;

namespace Sample.Core.Mapper
{
    public class SampleChildMappingProfile : Profile
    {
        public SampleChildMappingProfile()
        {
            CreateMap<SampleTestChild, SampleChildResponse>();
            CreateMap<RequestSampleChildCreate, SampleTestChild>();
        }
    }
}
