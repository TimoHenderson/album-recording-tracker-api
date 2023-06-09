using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Services
{
    public interface IEntityService<TEntity, TEntityDTO>
        where TEntity : IEntityBase
        where TEntityDTO : IEntityBaseDTO
    {
        public Task<IEnumerable<TEntityDTO>> GetAll(string userId);

        public Task<TEntityDTO?> Get(string userId, int id);

        public Task<TEntityDTO?> Create(string userId, TEntityDTO entity);

        public Task<TEntityDTO?> Delete(string userId, int id);

        public Task<TEntityDTO?> Update(string userId, TEntityDTO entity);

    }
}

