﻿using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Models;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApi.Controllers
{
   
    public abstract class GenericController<TEntity> :ControllerBase
		where TEntity:IEntityBase
	{
		protected readonly IEntityService<TEntity> _service;

		protected GenericController(IEntityService<TEntity> service)
		{
			_service = service;
		}

        [EnableCors]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var entities = await _service.GetAll();
            if (entities == null)
            {
                return NotFound();
            }
            return Ok(entities);
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>>? Get(int id)
        {
            var entity = await _service.Get(id);
            
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            var savedEntity = await _service.Create(entity);
            if (savedEntity == null)
            {
                return Problem("Error - not created");
            }

            return CreatedAtAction("Get", new { id = savedEntity.Id }, entity);
        }

    }
}

