using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;


public class Artist : TreeNode
{
    [NotMapped]
    public override string Type => "Artist";

    [NotMapped]
    public override string ChildType => "Album";

    [JsonIgnore]
    public ICollection<Album> Children { get; set; } = new List<Album>();

    [NotMapped]
    public ICollection<int> ChildrenIds => Children.Select(a => a.Id).ToList();

    [NotMapped]
    public override int? CalculatedCompletion { get => Children.Count > 0 ? Children.Sum(a => a.CalculatedCompletion) / Children.Count : null; }
}