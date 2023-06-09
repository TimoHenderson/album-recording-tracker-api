using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;


public abstract class GenericEntity : IEntityBase
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    // [JsonIgnore]
    // public ApplicationUser User { get; set; }

}