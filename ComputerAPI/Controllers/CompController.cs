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
            return Ok(await computerContext.Comps.ToListAsync());
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
                existingComp.OsId = updateCompDto.OsId;

                computerContext.Comps.Update(existingComp);
                await computerContext.SaveChangesAsync();
                return StatusCode(200, existingComp);
            }
            return StatusCode(404);
        }
    }
}
