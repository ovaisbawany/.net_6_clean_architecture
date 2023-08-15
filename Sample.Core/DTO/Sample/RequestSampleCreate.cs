
using Sample.Core.DTO.SampleChild;

namespace Sample.Core.DTO.Sample
{
    public class RequestSampleCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public List<RequestSampleChildCreate> SampleTestChild { get; set; }
    }
}
