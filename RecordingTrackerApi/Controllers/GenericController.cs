using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RecordingTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public abstract class GenericController<TEntity, TEntityDTO> : ControllerBase
        where TEntity : IEntityBase
        where TEntityDTO : IEntityBaseDTO

    {
        protected readonly IEntityService<TEntity, TEntityDTO> _service;

        protected GenericController(IEntityService<TEntity, TEntityDTO> service)
        {
            _service = service;
        }

        [EnableCors]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntityDTO>>> GetAll()
        {
            var entities = await _service.GetAll(UserId);
            if (entities == null)
            {
                return NotFound();
            }
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDTO>>? Get(int id)
        {
            var result = await _service.Get(UserId, id);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<TEntityDTO>> Post(TEntityDTO entity)
        {
            var result = await _service.Create(UserId, entity);
            if (!result.Success)
            {
                return Problem("Error - not created");
            }

            return CreatedAtAction("Get", new { id = result.Value.Id }, result.Value);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntityDTO entity)
        {
            entity.Id = id;
            var result = await _service.Update(UserId, entity);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await _service.Delete(UserId, id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Value);
        }

        private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // private string UserId => "1";

    }
}

