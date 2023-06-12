using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;


public abstract class GenericEntity : IEntityBase
{
    [Required]
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    public string? AspNetUserId { get; set; }

    public bool Starred { get; set; } = false;

    public string Notes { get; set; } = "";

}