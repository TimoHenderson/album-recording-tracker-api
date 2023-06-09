using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using AutoMapper;

namespace RecordingTrackerApi.Services
{
    public abstract class GenericEntityService<TEntity, TEntityDTO>
        : IEntityService<TEntity, TEntityDTO>
        where TEntity : GenericEntity
        where TEntityDTO : IEntityBaseDTO, new()
    {
        protected readonly RecordingContext _context;
        protected DbSet<TEntity> _dbSet;
        private readonly Expression<Func<TEntity, TEntityDTO>> _projectionCriteria;
        private readonly IMapper _mapper;

        public GenericEntityService(RecordingContext context,
            Expression<Func<TEntity, TEntityDTO>> projectionCriteria,
            MapperConfiguration mapperConfiguration)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _projectionCriteria = projectionCriteria;
            _mapper = mapperConfiguration.CreateMapper();
        }

        public virtual async Task<IEnumerable<TEntityDTO>> GetAll(string userId)
        {
            return await _dbSet
                .Select(_projectionCriteria)
                .ToListAsync();

        }

        public virtual async Task<TEntityDTO?> Get(string userId, int id)
        {
            return await _dbSet
                .Where(e => e.Id == id)
                .Select(_projectionCriteria)
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
            throw new NotImplementedException();
            // var updatedEntity = _context.Update(entity).Entity;
            // await _context.SaveChangesAsync();
            // return await Get(userId, updatedEntity.Id);
        }


        public async Task<TEntityDTO?> Delete(string userId, int id)
        {
            throw new NotImplementedException();
            // var entity = await _dbSet.FindAsync(id);
            // if (entity == null) return null;
            // _dbSet.Remove(entity);
            // await _context.SaveChangesAsync();
            // return entity;
        }

        public virtual Task<bool> ValidateRelationshipsAndAttach(TEntity entity) => Task.FromResult(true);

    }
}

