using System.ComponentModel.DataAnnotations;

namespace RecordingTrackerApi.Models.RecordingEntities.DTOs;
public class PartDTO : GenericEntityDTO
{
    [Required]
    public int SongId { get; set; }
    [Required]
    public int InstrumentId { get; set; }

    [Required]
    public int Completion { get; set; } = 0;
}
