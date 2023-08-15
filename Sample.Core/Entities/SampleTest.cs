using Sample.Core.Entities.Base;

namespace Sample.Core.Entities
{
    // Sample entity 
    public class SampleTest : BaseEntity<long>
    {
        public SampleTest()
        {

        }
        public SampleTest(List<SampleTestChild> sampleTestChildren)
        {
            SampleTestChild = sampleTestChildren;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public List<SampleTestChild> SampleTestChild { get; set; }
    }
}
