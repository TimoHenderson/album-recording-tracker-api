using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecordingTrackerApi.Models.RecordingEntities;

public abstract class TreeNode : GenericEntity
{
    public virtual string ChildType { get; } = "None";

}