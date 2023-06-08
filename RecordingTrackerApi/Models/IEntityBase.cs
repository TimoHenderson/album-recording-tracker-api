using System;
namespace RecordingTrackerApi.Models
{
    public interface IEntityBase
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

    }
}

