using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApi.Services
{
    public abstract class GenericEntityService<TEntity, TEntityDTO>
        : IEntityService<TEntity, TEntityDTO>
        where TEntity : GenericEntity
        where TEntityDTO : IEntityBaseDTO, new()
    {
        protected readonly RecordingContext _context;
        protected DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public GenericEntityService(RecordingContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntityDTO, TEntity>();
                cfg.CreateMap<TEntity, TEntityDTO>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        public virtual async Task<IEnumerable<TEntityDTO>> GetAll(string userId)
        {
            return await _dbSet
            .Where(e => e.AspNetUserId == userId || e.AspNetUserId == null)
                .ProjectTo<TEntityDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

        }

        public virtual async Task<Result<TEntityDTO>> Get(string userId, int id)
        {
            var entityDTO = await _dbSet
                .Where(e => e.Id == id && (e.AspNetUserId == userId || e.AspNetUserId == null))
                .ProjectTo<TEntityDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (entityDTO == null) return Result<TEntityDTO>.Fail("Not found");
            return Result<TEntityDTO>.Ok(entityDTO);
        }


        public virtual async Task<Result<TEntityDTO>> Create(string userId, TEntityDTO entityDTO)
        {
            var validateRelationshipsResult = await ValidateRelationships(userId, entityDTO);
            if (!validateRelationshipsResult.IsValid)
                return Result<TEntityDTO>.Fail(validateRelationshipsResult.ErrorMessage);

            var entity = _mapper.Map<TEntity>(entityDTO);
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            var savedDTO = _mapper.Map<TEntityDTO>(entity);
            return Result<TEntityDTO>.Ok(savedDTO);
        }

        public virtual async Task<Result<TEntityDTO>> Update(string userId, TEntityDTO entityDTO)
        {
            var validateRelationshipsResult = await ValidateRelationships(userId, entityDTO);
            if (!validateRelationshipsResult.IsValid)
                return Result<TEntityDTO>.Fail(validateRelationshipsResult.ErrorMessage);

            var storedEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == entityDTO.Id);
            if (storedEntity == null) return Result<TEntityDTO>.Fail("Not found");

            if (storedEntity.AspNetUserId != userId) return Result<TEntityDTO>.Fail("Not owned by user");

            _mapper.Map(entityDTO, storedEntity);
            var updatedEntity = _context.Update(storedEntity).Entity;
            await _context.SaveChangesAsync();
            return Result<TEntityDTO>.Ok(_mapper.Map<TEntityDTO>(updatedEntity));
        }


        public async Task<Result<TEntityDTO>> Delete(string userId, int id)
        {
            var storedEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (storedEntity == null) return Result<TEntityDTO>.Fail("Not found");

            if (storedEntity.AspNetUserId != userId) return Result<TEntityDTO>.Fail("Not owned by user");
            _dbSet.Remove(storedEntity);
            await _context.SaveChangesAsync();

            return Result<TEntityDTO>.Ok(_mapper.Map<TEntityDTO>(storedEntity));
        }

        public virtual Task<ValidateRelationshipsResult> ValidateRelationships(string UserId, TEntityDTO entityDTO)
        {
            return Task.FromResult(ValidateRelationshipsResult.Valid());
        }

    }
}

