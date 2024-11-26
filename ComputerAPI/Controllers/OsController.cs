using ComputerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public OsController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<OSystem>> Post(CreateOsDto createOsDto)
        {
            var os = new OSystem
            {
                Id = Guid.NewGuid(),
                Name = createOsDto.Name
            };

            if (os != null)
            {
                await computerContext.Os.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os);
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult<OSystem>> Get()
        {
            return Ok(await computerContext.Os.ToListAsync());
        }
        [HttpGet("id")]
        public async Task<ActionResult<OSystem>> GetId(Guid id)
        {
            var os = await computerContext.Os.FirstOrDefaultAsync(x => id == x.Id);
            if (os != null)
            {
                return Ok(os);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<OSystem>> Put(UpdateOsDto updateOsDto, Guid id)
        {

            var existingOs = await computerContext.Os.FirstOrDefaultAsync(x => id == x.Id);

            if (existingOs != null)
            {
                existingOs.Name = updateOsDto.Name;
                computerContext.Os.Update(existingOs);
                await computerContext.SaveChangesAsync();
                return StatusCode(200, existingOs);
            }
            return StatusCode(404);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var os = await computerContext.Os.FirstOrDefaultAsync(x => id == x.Id);
            if (os != null)
            {
                computerContext.Os.Remove(os);
                await computerContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
        [HttpGet("withAllComputer")]
        public async Task<ActionResult<OSystem>> GetWithAllComputer()
        {
            return Ok(await computerContext.Os.Include(os => os.Comps).ToListAsync());
        }
    }
}
