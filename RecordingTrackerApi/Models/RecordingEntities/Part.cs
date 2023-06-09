using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;


public class Part : TreeNode
{
    private int parentNum;
    private int instrumentNum;

    [JsonIgnore]
    public Song? Parent { get; set; } = null;

    [NotMapped]
    public string? ParentType => Parent != null ? Parent.Type : null;

    [NotMapped]
    public int ParentNum
    {
        get { return Parent != null ? Parent.Id : parentNum; }
        set { parentNum = value; }
    }

    [NotMapped]
    public int InstrumentNum
    {
        get { return Instrument != null ? Instrument.Id : instrumentNum; }
        set { instrumentNum = value; }
    }

    [NotMapped]
    public override string Type => "Part";

    [JsonIgnore]
    public Instrument? Instrument { get; set; } = null;


    [JsonIgnore]
    public int Completion { get; set; } = 0;

    [NotMapped]
    public override int? CalculatedCompletion { get => Completion; }

}