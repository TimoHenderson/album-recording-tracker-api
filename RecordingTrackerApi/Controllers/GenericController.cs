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
            var entity = await _service.Get(UserId, id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TEntityDTO>> Post(TEntityDTO entity)
        {
            var savedEntity = await _service.Create(UserId, entity);
            if (savedEntity == null)
            {
                return Problem("Error - not created");
            }

            return CreatedAtAction("Get", new { id = savedEntity.Id }, savedEntity);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntityDTO entity)
        {
            var updatedEntity = await _service.Update(UserId, entity);
            if (updatedEntity == null) return BadRequest();
            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var deletedEntity = await _service.Delete(UserId, id);
            if (deletedEntity == null) return BadRequest();
            return Ok(deletedEntity);
        }

        private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // private string UserId => "1";

    }
}

