using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using RecordingTrackerApi.Models.RecordingEntities;

namespace RecordingTrackerApiTests;

[Collection("DatabaseCollection")]
public class InMemoryDatabaseFixture : IDisposable
{
    public RecordingContext Context { get; private set; }

    public InMemoryDatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<RecordingContext>()
            .UseInMemoryDatabase(databaseName: "RecordingTrackerTestDatabase")
            .Options;

        Context = new RecordingContext(options);

        IList<Instrument> instruments = new List<Instrument>{
            new Instrument { Id = 1, Name = "Guitar" ,AspNetUserId = "1"},
            new Instrument { Id = 2, Name = "Bass" ,AspNetUserId = "1"},
            new Instrument { Id = 3, Name = "Drums",    AspNetUserId = "2" },
        };
        Context.Instruments.AddRange(instruments);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context = null;
    }
}

