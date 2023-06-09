using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;

public abstract class TreeNode : GenericEntity
{
    [NotMapped]
    public abstract string Type { get; }

    public virtual string ChildType { get; } = "None";

    public virtual int? CalculatedCompletion { get; } = null;

}