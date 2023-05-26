using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models;

public class Song
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [JsonIgnore]
    public Album Album { get; set; } = new Album();

    public ICollection<Part> Parts { get; set; } = new List<Part>();
}