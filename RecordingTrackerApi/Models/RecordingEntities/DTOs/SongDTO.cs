using System.ComponentModel.DataAnnotations;

namespace RecordingTrackerApi.Models.RecordingEntities.DTOs;
public class SongDTO : GenericEntityDTO
{
    [Required]
    public int ParentId { get; set; }
    public override string Type => "song";
}
