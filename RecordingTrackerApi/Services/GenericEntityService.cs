using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

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

        public GenericEntityService(RecordingContext context, Expression<Func<TEntity, TEntityDTO>> projectionCriteria)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _projectionCriteria = projectionCriteria;
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


        public virtual async Task<TEntityDTO?> Create(string userId, TEntityDTO entity)
        {
            throw new NotImplementedException();
            // _dbSet.Add(entity);
            // await _context.SaveChangesAsync();
            // return entity;
        }

        public virtual async Task<TEntityDTO?> Update(string userId, TEntityDTO entity)
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

        private static TEntityDTO CreateDtoFromEntity(TEntity entity)
        {
            var dto = new TEntityDTO();
            PropertyInfo[] dtoProperties = typeof(TEntityDTO).GetProperties();
            PropertyInfo[] entityProperties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {

                if (dtoProperty.Name == "ParentId")
                {
                    PropertyInfo? parentProperty = entityProperties.FirstOrDefault(p => p.Name == "Parent");
                    if (parentProperty != null)
                    {
                        object parentValue = parentProperty.GetValue(entity);
                        if (parentValue != null)
                        {
                            PropertyInfo parentIdProperty = parentProperty.PropertyType.GetProperty("Id");
                            if (parentIdProperty != null)
                            {
                                object parentIdValue = parentIdProperty.GetValue(parentValue);
                                dtoProperty.SetValue(dto, parentIdValue);
                            }
                        }
                    }
                }
                else
                {
                    PropertyInfo correspondingEntityProperty = entityProperties.FirstOrDefault(p => p.Name == dtoProperty.Name);
                    if (correspondingEntityProperty != null)
                    {
                        object value = correspondingEntityProperty.GetValue(entity);
                        if (dtoProperty.SetMethod != null)
                        {
                            dtoProperty.SetValue(dto, value);
                        }
                    }
                }
            }

            return dto;


        }
    }
}

