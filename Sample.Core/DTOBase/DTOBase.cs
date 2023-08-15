using System;


namespace Sample.Core.DTOBase
{
    public class DtoBase
    {
        public bool HasError { get; set; }

        public List<String> Errors { get; set; }
    }
}
