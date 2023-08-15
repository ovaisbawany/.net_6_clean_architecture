using System;

namespace Sample.Core.DTO.Sample
{
    // Sample response or DTO class
    public class SampleResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public List<SampleChildResponse> SampleTestChild { get; set; }
    }
}
