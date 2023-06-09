using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;

public class Album : TreeNode
{

    private int parentNum;

    //[JsonIgnore]
    public Artist? Parent { get; set; } = null;

    [NotMapped]
    public string? ParentType => Parent != null ? Parent.Type : null;

    [NotMapped]
    public int ParentNum
    {
        get { return Parent != null ? Parent.Id : parentNum; }
        set { parentNum = value; }
    }

    [NotMapped]
    public override string Type => "Album";

    [NotMapped]
    public override string ChildType => "Song";

    [JsonIgnore]
    public ICollection<Song> Children { get; set; } = new List<Song>();

    [NotMapped]
    public ICollection<int> ChildrenIds => Children.Select(a => a.Id).ToList();

    [NotMapped]
    public override int? CalculatedCompletion { get => Children.Count > 0 ? Children.Sum(a => a.CalculatedCompletion) / Children.Count : null; }

}