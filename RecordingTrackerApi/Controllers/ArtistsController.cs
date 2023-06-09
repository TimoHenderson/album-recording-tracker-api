using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Services;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Controllers
{
    public class ArtistsController : GenericController<Artist, ArtistDTO>
    {
        public ArtistsController(ArtistsService service) : base(service) { }

    }
}
