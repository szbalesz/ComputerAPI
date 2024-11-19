﻿using ComputerAPI.Models;
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
            return Ok(await computerContext.Os.FirstOrDefaultAsync(x => id == x.Id));
        }
    }
}
