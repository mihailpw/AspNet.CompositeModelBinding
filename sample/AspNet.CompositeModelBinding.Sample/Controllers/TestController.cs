using Microsoft.AspNetCore.Mvc;

namespace AspNet.CompositeModelBinding.Sample.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([BindSource] Dto dto)
        {
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([BindSource] Dto dto)
        {
            return Ok(dto);
        }

        [HttpGet("{id}/{id2}")]
        public IActionResult GetByIdAndId2([BindSource] Dto dto)
        {
            return Ok(dto);
        }

        [HttpPost("{id?}/{id2?}")]
        public IActionResult PostByIdAndId2([BindSource] Dto dto)
        {
            return Ok(dto);
        }

        [HttpPut("{id?}/{id2?}")]
        public IActionResult PutByIdAndId2([BindSource] Dto dto)
        {
            return Ok(dto);
        }

        [HttpPatch("{id?}/{id2?}")]
        public IActionResult PatchByIdAndId2([BindSource] Dto dto)
        {
            return Ok(dto);
        }

        [HttpDelete("{id?}/{id2?}")]
        public IActionResult DeleteByIdAndId2([BindSource] Dto dto)
        {
            return Ok(dto);
        }
    }
}