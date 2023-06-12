using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;

public class Song : TreeNode
{
    public int AlbumId { get; set; }
    public Album Album { get; set; } = null!;
    public ICollection<Part> Parts { get; set; } = new List<Part>();

}