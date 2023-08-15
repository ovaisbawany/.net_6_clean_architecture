using System.ComponentModel.DataAnnotations;

namespace Sample.Core.DTO.SampleChild
{
    public class RequestSampleChildCreate
    {
        public long SampleTestId { get; set; }

        [MaxLength(20)]
        public string ChildName { get; set; }
        public string Password { get; set; }
        public int ChildAge { get; set; }
        public string ChildSchool { get; set; }
        public string ChildAddress { get; set; }
    }
}
