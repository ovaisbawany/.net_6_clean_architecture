using System;

namespace Sample.Core.DTO.Sample
{
    // Sample response or DTO class
    public class SampleChildResponse
    {
        public long Id { get; set; }
        public long SampleTestId { get; set; }
        public string ChildName { get; set; }
        public int ChildAge { get; set; }
        public string ChildSchool { get; set; }
        public string ChildAddress { get; set; }
    }
}
