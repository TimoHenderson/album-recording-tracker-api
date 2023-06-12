using System.ComponentModel.DataAnnotations;

namespace RecordingTrackerApi.Models.RecordingEntities.DTOs;
public class SongDTO : GenericEntityDTO
{
    [Required]
    public int AlbumId { get; set; }

}
