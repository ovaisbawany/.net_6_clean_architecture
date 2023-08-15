using Sample.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Sample.Core.Entities
{
    // Sample entity 
    public class SampleTestChild : BaseEntity<long>
    {
        public SampleTest SampleTest { get; set; }
        public long SampleTestId { get; set; }
        
        [MaxLength(20)]
        public string ChildName { get; set; }
        public int ChildAge { get; set; }
        public string ChildSchool { get; set; }
        public string ChildAddress { get; set; }        

    }
}
