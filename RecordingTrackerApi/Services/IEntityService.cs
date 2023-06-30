using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApi.Services
{
    public interface IEntityService<TEntity, TEntityDTO>
        where TEntity : IEntityBase
        where TEntityDTO : IEntityBaseDTO
    {
        public Task<IEnumerable<TEntityDTO>> GetAll(string userId);

        public Task<Result<TEntityDTO>> Get(string userId, int id);

        public Task<Result<TEntityDTO>> Create(string userId, TEntityDTO entity);

        public Task<Result<TEntityDTO>> Delete(string userId, int id);

        public Task<Result<TEntityDTO>> Update(string userId, TEntityDTO entity);

    }
}

