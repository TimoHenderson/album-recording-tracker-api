using System.ComponentModel.DataAnnotations;
namespace RecordingTrackerApi.Models.RecordingEntities.DTOs;
public class AlbumDTO : GenericEntityDTO
{
    [Required]
    public int ArtistId { get; set; }
}
