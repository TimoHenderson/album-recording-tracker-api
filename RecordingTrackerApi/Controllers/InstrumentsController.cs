using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using RecordingTrackerApi.Data.Helpers;
using RecordingTrackerApi.Models;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApi.Controllers
{

    public class InstrumentsController : GenericController<Instrument>
    {
        public InstrumentsController(InstrumentsService service) : base(service) { }
    }
}

