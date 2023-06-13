


using Moq.EntityFrameworkCore;
using RecordingTrackerApi.Data;
using RecordingTrackerApi.Models.RecordingEntities;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApiTests;


public class InstrumentServiceTest
{
    private readonly InstrumentsService service;

    public InstrumentServiceTest()
    {
        var context = new InMemoryDatabaseFixture().CreateContext();
        service = new InstrumentsService(context);
    }

    [Theory]
    [InlineData("1", 7)]
    [InlineData("2", 6)]
    public void CanGetAllInstruments(string userId, int expected)
    {
        // Act
        var instruments = service.GetAll(userId).Result;

        // Assert
        Assert.Equal(expected, instruments.Count());
    }

    [Fact]
    public void CanGetInstrumentById()
    {
        // Act
        var instrument = service.Get("1", 1).Result;

        // Assert
        var expected = new InstrumentDTO { Id = 1, Name = "Guitar", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, instrument.Id);
        Assert.Equal(expected.Name, instrument.Name);
        Assert.Equal(expected.Starred, instrument.Starred);
        Assert.Equal(expected.Notes, instrument.Notes);
        Assert.Equal("Instrument", instrument.Type);
    }

    [Fact]
    public void CanCreateInstrument()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 0, Name = "Piano" };

        // Act
        var result = service.Create("1", instrument).Result;

        // Assert
        var expected = new InstrumentDTO { Id = 9, Name = "Piano", Starred = false, Notes = "" };

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
    public void UpdateInstrumentWithInvalidIdReturnsNull()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 4, Name = "Egg" };

        // Act
        var result = service.Update("1", instrument).Result;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CanDeleteInstrument()
    {
        // Act
        var initialNumInstruments = service.GetAll("1").Result.Count();
        service.Delete("1", 1);

        var afterNumInstruments = service.GetAll("1").Result.Count();
        var deletedInstrument = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedInstrument);
        Assert.Equal(initialNumInstruments - 1, afterNumInstruments);
    }
    [Fact]
    public void CanNotDeleteInstrumentWithInvalidId()
    {

        var result = service.Delete("1", 5);

        Assert.Null(result.Result);

    }
}
