using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;


public class Part : TreeNode
{
    public int SongId { get; set; }
    public Song Song { get; set; } = null!;
    public int InstrumentId { get; set; }
    public Instrument Instrument { get; set; } = null!;
    public int Completion { get; set; } = 0;
}