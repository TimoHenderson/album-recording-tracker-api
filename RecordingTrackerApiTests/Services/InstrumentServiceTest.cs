


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
        var dto = instrument.Value;

        // Assert
        var expected = new InstrumentDTO { Id = 1, Name = "Guitar", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Instrument", dto.Type);
    }

    [Fact]
    public void CanCreateInstrument()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 0, Name = "Piano" };

        // Act
        var result = service.Create("1", instrument).Result;
        var dto = result.Value;

        // Assert
        var expected = new InstrumentDTO { Id = 9, Name = "Piano", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Instrument", dto.Type);
    }

    [Fact]
    public void CanUpdateInstrument()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("1", instrument).Result;
        var dto = result.Value;

        var result2 = service.Get("1", 1).Result;
        var dto2 = result2.Value;

        // Assert
        var expected = new InstrumentDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Instrument", dto.Type);
        Assert.Equal(expected.Id, dto2.Id);
        Assert.Equal(expected.Name, dto2.Name);
        Assert.Equal(expected.Starred, dto2.Starred);
        Assert.Equal(expected.Notes, dto2.Notes);
        Assert.Equal("Instrument", dto2.Type);
        Assert.True(result.Success);
    }

    [Fact]
    public void UpdateInstrumentWithInvalidIdReturnsNull()
    {
        // Arrange
        var instrument = new InstrumentDTO { Id = 4, Name = "Egg" };

        // Act
        var result = service.Update("1", instrument).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
    }

    [Fact]
    public void CanDeleteInstrument()
    {
        // Act
        var initialNumInstruments = service.GetAll("1").Result.Count();
        var result = service.Delete("1", 1).Result;

        var afterNumInstruments = service.GetAll("1").Result.Count();
        var deletedInstrument = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedInstrument.Value);
        Assert.Equal(initialNumInstruments - 1, afterNumInstruments);
        Assert.True(result.Success);

    }
    [Fact]
    public void CanNotDeleteInstrumentWithInvalidId()
    {

        var result = service.Delete("1", 10).Result;

        Assert.Null(result.Value);
        Assert.False(result.Success);
        Assert.Equal("Not found", result.ErrorMessage);

    }
}
