using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecordingTrackerApi.Models.RecordingEntities;

public class Album : TreeNode
{
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
    public ICollection<Song> Songs { get; set; } = new List<Song>();

}