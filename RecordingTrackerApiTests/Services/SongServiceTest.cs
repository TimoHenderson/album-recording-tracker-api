using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApiTests;


public class SongServiceTest
{
    private readonly SongsService service;

    public SongServiceTest()
    {
        var context = new InMemoryDatabaseFixture().CreateContext();
        service = new SongsService(context);
    }

    [Theory]
    [InlineData("1", 4)]
    [InlineData("2", 9)]
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
        var expected = new SongDTO { Id = 1, Name = "Song 1", Starred = false, Notes = "", AlbumId = 1 };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.AlbumId, dto.AlbumId);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Song", dto.Type);
    }

    [Fact]
    public void CanCreate()
    {
        // Arrange
        var entityDTO = new SongDTO { Id = 0, Name = "Timothy", AlbumId = 1 };

        // Act
        var result = service.Create("1", entityDTO).Result;
        var dto = result.Value;

        // Assert
        var expected = new SongDTO { Name = "Timothy", Starred = false, Notes = "", AlbumId = 1 };

        Assert.NotEqual(0, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Song", dto.Type);
    }

    [Fact]
    public void CanNotCreateIfAlbumIsNotOwnedByUser()
    {
        // Arrange
        var entityDTO = new SongDTO { Id = 0, Name = "Timothy", AlbumId = 4 };

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
        var entityDTO = new SongDTO { Id = 1, Name = "Egg", AlbumId = 1 };

        // Act
        var result = service.Update("1", entityDTO).Result;
        var dto = result.Value;

        var result2 = service.Get("1", 1).Result;
        var dto2 = result2.Value;

        // Assert
        var expected = new SongDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Song", dto.Type);
        Assert.Equal(expected.Id, dto2.Id);
        Assert.Equal(expected.Name, dto2.Name);
        Assert.Equal(expected.Starred, dto2.Starred);
        Assert.Equal(expected.Notes, dto2.Notes);
        Assert.Equal("Song", dto2.Type);
    }

    [Fact]
    public void UpdateWithInvalidIdReturnsNull()
    {
        // Arrange
        var entityDTO = new SongDTO { Id = 25, Name = "Egg" };

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
        var entityDTO = new SongDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("5", entityDTO).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
    }

    [Fact]
    public void UpdateToSongThatIsNotOwnedReturnsNull()
    {
        // Arrange
        var entityDTO = new SongDTO { Id = 1, Name = "Egg", AlbumId = 4 };

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
        var initialNumSongs = service.GetAll("1").Result.Count();
        var result = service.Delete("1", 1).Result;

        var afterNumSongs = service.GetAll("1").Result.Count();
        var deletedSongs = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedSongs.Value);
        Assert.Equal(initialNumSongs - 1, afterNumSongs);
        Assert.True(result.Success);
    }
    [Fact]
    public void CanNotDeleteAWithInvalidId()
    {
        var result = service.Delete("1", 25).Result;
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

