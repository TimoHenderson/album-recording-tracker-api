using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
            .Where(e => e.AspNetUserId == userId)
                .ProjectTo<TEntityDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

        }

        public virtual async Task<TEntityDTO?> Get(string userId, int id)
        {
            return await _dbSet
                .Where(e => e.Id == id)
                .ProjectTo<TEntityDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }


        public virtual async Task<TEntityDTO?> Create(string userId, TEntityDTO entityDTO)
        {
            var entity = _mapper.Map<TEntity>(entityDTO);
            if (!await ValidateRelationshipsAndAttach(entity)) return default;
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TEntityDTO>(entity);
        }

        public virtual async Task<TEntityDTO?> Update(string userId, TEntityDTO entityDTO)
        {
            var storedEntity = await _dbSet.FirstOrDefaultAsync(
                e => e.Id == entityDTO.Id &&
                e.AspNetUserId == userId);

            if (storedEntity == null) return default;

            _mapper.Map(entityDTO, storedEntity);
            var updatedEntity = _context.Update(storedEntity).Entity;
            await _context.SaveChangesAsync();
            return _mapper.Map<TEntityDTO>(updatedEntity);
        }


        public async Task<TEntityDTO?> Delete(string userId, int id)
        {
            var storedEntity = await _dbSet.FirstOrDefaultAsync(
                e => e.Id == id &&
                e.AspNetUserId == userId);
            if (storedEntity == null) return default;
            _dbSet.Remove(storedEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TEntityDTO>(storedEntity);
        }

        public virtual Task<bool> ValidateRelationshipsAndAttach(TEntity entity) => Task.FromResult(true);

    }
}

