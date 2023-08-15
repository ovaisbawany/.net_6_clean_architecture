using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
        protected virtual IActionResult JsonResponse<T>(dynamic obj)
        {
            if (obj.HasError)
                return BadRequest(obj);
            return Ok(obj);
        }
    }
}
