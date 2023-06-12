namespace RecordingTrackerApi.Models.RecordingEntities.DTOs;
using System.ComponentModel.DataAnnotations;
public abstract class GenericEntityDTO : IEntityBaseDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public string Type { get; } = null!;
}
