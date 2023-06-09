using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;

public class Song : TreeNode
{
    private int parentNum;

    [NotMapped]
    public override string Type => "Song";

    [NotMapped]
    public override string ChildType => "Part";

    [JsonIgnore]
    public Album? Parent { get; set; } = null;

    [NotMapped]
    public int ParentNum
    {
        get { return Parent != null ? Parent.Id : parentNum; }
        set { parentNum = value; }
    }

    [NotMapped]
    public string? ParentType => Parent != null ? Parent.Type : null;

    [JsonIgnore]
    public ICollection<Part> Children { get; set; } = new List<Part>();

    [NotMapped]
    public ICollection<int> ChildrenIds => Children.Select(a => a.Id).ToList();


    [NotMapped]
    public override int? CalculatedCompletion { get => Children.Count > 0 ? Children.Sum(a => a.CalculatedCompletion) / Children.Count : null; }

}