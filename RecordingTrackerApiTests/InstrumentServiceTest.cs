


using Moq.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApiTests;

[Collection("DatabaseCollection")]
public class InstrumentServiceTest : IDisposable
{
    InstrumentsService service;
    InMemoryDatabaseFixture _fixture;

    public InstrumentServiceTest(InMemoryDatabaseFixture fixture)
    {
        _fixture = fixture;
        service = new InstrumentsService(fixture.Context);
    }
    public void Dispose()
    {
        service = null;
    }

    [Theory]
    [InlineData("1", 2)]
    [InlineData("2", 1)]
    public void CanGetAllInstruments(string userId, int expected)
    {
        // Act
        var instruments = service.GetAll(userId).Result;

        // Assert
        Assert.Equal(expected, instruments.Count());
    }

    [Fact]
    public void CanCreateInstrument()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 0, Name = "Piano" };

        // Act
        var result = service.Create("1", instrument).Result;

        // Assert
        var expected = new InstrumentDTO { Id = 4, Name = "Piano", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Instrument", result.Type);
    }

    [Fact]
    public void CanUpdateInstrument()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("1", instrument).Result;

        var result2 = service.Get("1", 1).Result;

        // Assert
        var expected = new InstrumentDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Instrument", result.Type);
        Assert.Equal(expected.Id, result2.Id);
        Assert.Equal(expected.Name, result2.Name);
        Assert.Equal(expected.Starred, result2.Starred);
        Assert.Equal(expected.Notes, result2.Notes);
        Assert.Equal("Instrument", result2.Type);
    }

    [Fact]
    public void CanDeleteInstrument()
    {
        // Act
        service.Delete("1", 1);

        var instruments = service.GetAll("1").Result;
        var deletedInstrument = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedInstrument);
        Assert.Equal(1, instruments.Count());
    }
}
