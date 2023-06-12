using System;
using RecordingTrackerApi.Models.Users;

namespace RecordingTrackerApi.Models.RecordingEntities
{
    public interface IEntityBase
    {
        public int Id { get; set; }

        public string? AspNetUserId { get; set; }

    }
}

