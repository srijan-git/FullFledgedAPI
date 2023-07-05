using FullFledgedAPI.Modal;
using FullFledgedAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullFledgedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode(int code)
        {
            var data = await _service.GetByCode(code);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(Customermodal _data)
        {
            var data = await _service.Create(_data);
            return Ok(data);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(Customermodal _data, int code)
        {
            var data = await _service.Update(_data, code);
            return Ok(data);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int code)
        {
            var data = await _service.Remove(code);
            return Ok(data);
        }
    }
}
