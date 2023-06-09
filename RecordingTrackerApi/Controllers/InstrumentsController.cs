
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Services;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;

namespace RecordingTrackerApi.Controllers
{

    public class InstrumentsController : GenericController<Instrument, InstrumentDTO>
    {
        public InstrumentsController(InstrumentsService service) : base(service) { }
    }
}

