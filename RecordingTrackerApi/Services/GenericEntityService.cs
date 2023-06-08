using System;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models;

namespace RecordingTrackerApi.Services
{
    public abstract class GenericEntityService<TEntity>
        : IEntityService<TEntity> where TEntity : GenericEntity
    {
        protected readonly RecordingContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericEntityService(RecordingContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(string userId)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<TEntity?> Get(string userId, int id)
        {
            return await _dbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
        }


        public virtual async Task<TEntity?> Create(string userId, TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity?> Update(string userId, TEntity entity)
        {
            var updatedEntity = _context.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return await Get(updatedEntity.Id);
        }


        public async Task<TEntity?> Delete(string userId, int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }



    }
}

