using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;


public class Artist : TreeNode
{
    public ICollection<Album> Albums { get; set; } = new List<Album>();

}