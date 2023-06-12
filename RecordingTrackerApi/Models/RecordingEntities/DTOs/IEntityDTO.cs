namespace RecordingTrackerApi.Models.RecordingEntities.DTOs;
public interface IEntityBaseDTO
{
    int Id { get; set; }
    string Name { get; set; }
    string Type { get; }

}
