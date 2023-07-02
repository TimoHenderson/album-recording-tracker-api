using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApiTests;


public class PartsServiceTest
{
    private readonly PartsService service;

    public PartsServiceTest()
    {
        var context = new InMemoryDatabaseFixture().CreateContext();
        service = new PartsService(context);
    }

    [Theory]
    [InlineData("1", 13)]
    [InlineData("2", 46)]
    public void CanGetAll(string userId, int expected)
    {
        // Act
        var entityDTOs = service.GetAll(userId).Result;

        // Assert
        Assert.Equal(expected, entityDTOs.Count());
    }

    [Fact]
    public void CanGetById()
    {
        // Act
        var result = service.Get("1", 1).Result;
        var dto = result.Value;

        // Assert
        var expected = new PartDTO { Id = 1, Name = "Guitar Part", Starred = false, Notes = "", SongId = 1 };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.SongId, dto.SongId);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Part", dto.Type);
    }

    [Fact]
    public void CanCreate()
    {
        // Arrange
        var entityDTO = new PartDTO { Id = 0, Name = "Timothy", SongId = 1, InstrumentId = 1 };

        // Act
        var result = service.Create("1", entityDTO).Result;
        var dto = result.Value;

        // Assert
        var expected = new PartDTO { Name = "Timothy", Starred = false, Notes = "", SongId = 1 };

        Assert.NotEqual(0, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Part", dto.Type);
    }

    [Fact]
    public void CanNotCreateIfPartIsNotOwnedByUser()
    {
        // Arrange
        var entityDTO = new PartDTO { Id = 0, Name = "Timothy", SongId = 4, InstrumentId = 1 };

        // Act
        var result = service.Create("1", entityDTO).Result;
        var dto = result.Value;

        // Assert
        Assert.Null(dto);
    }

    [Fact]
    public void CanUpdate()
    {
        // Arrange
        var entityDTO = new PartDTO { Id = 1, Name = "Egg", SongId = 1, InstrumentId = 1 };

        // Act
        var result = service.Update("1", entityDTO).Result;
        var dto = result.Value;

        var result2 = service.Get("1", 1).Result;
        var dto2 = result2.Value;

        // Assert
        var expected = new PartDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Part", dto.Type);
        Assert.Equal(expected.Id, dto2.Id);
        Assert.Equal(expected.Name, dto2.Name);
        Assert.Equal(expected.Starred, dto2.Starred);
        Assert.Equal(expected.Notes, dto2.Notes);
        Assert.Equal("Part", dto2.Type);
    }

    [Fact]
    public void UpdateWithInvalidIdReturnsNull()
    {
        // Arrange
        var entityDTO = new PartDTO { Id = 25, Name = "Egg" };

        // Act
        var result = service.Update("1", entityDTO).Result;


        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
    }

    [Fact]
    public void UpdateWithInvalidUserIdReturnsNull()
    {
        // Arrange
        var entityDTO = new PartDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("5", entityDTO).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
    }

    [Fact]
    public void UpdateToPartThatIsNotOwnedReturnsNull()
    {
        // Arrange
        var entityDTO = new PartDTO { Id = 1, Name = "Egg", SongId = 4, InstrumentId = 1 };

        // Act
        var result = service.Update("1", entityDTO).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
    }

    [Fact]
    public void CanDelete()
    {
        // Act
        var initialNumParts = service.GetAll("1").Result.Count();
        var result = service.Delete("1", 1).Result;

        var afterNumParts = service.GetAll("1").Result.Count();
        var deletedParts = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedParts.Value);
        Assert.Equal(initialNumParts - 1, afterNumParts);
        Assert.True(result.Success);
    }
    [Fact]
    public void CanNotDeleteAWithInvalidId()
    {
        var result = service.Delete("1", 120).Result;
        Assert.Null(result.Value);
        Assert.Equal("Not found", result.ErrorMessage);
    }

    [Fact]
    public void CanNotDeleteWithInvalidUserId()
    {
        var result = service.Delete("5", 1).Result;
        Assert.Null(result.Value);
        Assert.Equal("Not owned by user", result.ErrorMessage);
    }
}

