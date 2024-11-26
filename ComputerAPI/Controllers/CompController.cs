using ComputerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using System.Security.Cryptography;
using System;

namespace ComputerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompController : ControllerBase
    {

        private readonly ComputerContext computerContext;

        public CompController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<Comp>> Post(CreateCompDto createCompDto)
        {
            var comp = new Comp
            {
                Id = Guid.NewGuid(),
                Brand = createCompDto.Brand,
                Type = createCompDto.Type,
                Display = createCompDto.Display,
                Memory = createCompDto.Memory,
                OsId = createCompDto.OsId,
                CreatedTime = DateTime.Now
            };

            if (comp != null)
            {
                await computerContext.Comps.AddAsync(comp);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, comp);
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult<Comp>> Get()
        {
            return Ok(await computerContext.Comps.Select(x => new { x.Brand, x.Type, x.Memory, x.Os.Name }).ToListAsync());
        }

        [HttpGet("numberOfComputers")]
        public async Task<ActionResult> GetNumberOfComputers()
        {
            var c = await computerContext.Comps.ToListAsync();
            return Ok(new { message = "Sikeres lekérdezés!", result = c.Count() });
        }

        [HttpGet("allWindowsOsComputer")]
        public async Task<ActionResult<Comp>> GetAllWindowsOsComputer()
        {
            return Ok(await computerContext.Comps.Where(x => x.Os.Name.Contains("Windows")).Select(x=> new { comp = x,osName = x.Os.Name }).ToListAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult<Comp>> GetId(Guid id)
        {
            var comp = await computerContext.Comps.FirstOrDefaultAsync(x => id == x.Id);
            if (comp != null)
            {
                return Ok(comp);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<Comp>> Put(UpdateCompDto updateCompDto, Guid id)
        {

            var existingComp = await computerContext.Comps.FirstOrDefaultAsync(x => id == x.Id);

            if (existingComp != null)
            {
                existingComp.Brand = updateCompDto.Brand;
                existingComp.Type = updateCompDto.Type;
                existingComp.Display = updateCompDto.Display;
                existingComp.Memory = updateCompDto.Memory;

                computerContext.Comps.Update(existingComp);
                await computerContext.SaveChangesAsync();
                return StatusCode(200, existingComp);
            }
            return StatusCode(404);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var comp = await computerContext.Comps.FirstOrDefaultAsync(x => id == x.Id);
            if (comp != null)
            {
                computerContext.Comps.Remove(comp);
                await computerContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
