using System;
using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Models;

namespace RecordingTrackerApi.Services
{
    public interface IEntityService<T> where T : IEntityBase
    {
        public Task<IEnumerable<T>> GetAll(string userId);

        public Task<T?> Get(string userId, int id);

        public Task<T?> Create(string userId, T entity);

        public Task<T?> Delete(string userId, int id);

        public Task<T?> Update(string userId, T entity);

    }
}

