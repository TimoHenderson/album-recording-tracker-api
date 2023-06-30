using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;
using RecordingTrackerApi.Results;

namespace RecordingTrackerApiTests;


public class AlbumServiceTest
{
    private readonly AlbumsService service;

    public AlbumServiceTest()
    {
        var context = new InMemoryDatabaseFixture().CreateContext();
        service = new AlbumsService(context);
    }

    [Theory]
    [InlineData("1", 2)]
    [InlineData("2", 2)]
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
        var expected = new AlbumDTO { Id = 1, Name = "Album 1", Starred = false, Notes = "", ArtistId = 1 };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.ArtistId, dto.ArtistId);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Album", dto.Type);
    }

    [Fact]
    public void CanCreate()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 0, Name = "Timothy", ArtistId = 1 };

        // Act
        var result = service.Create("1", entityDTO).Result;
        var dto = result.Value;

        // Assert
        var expected = new AlbumDTO { Name = "Timothy", Starred = false, Notes = "", ArtistId = 1 };

        Assert.NotEqual(0, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Album", dto.Type);
    }

    [Fact]
    public void CanNotCreateIfArtistIsNotOwnedByUser()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 0, Name = "Timothy", ArtistId = 4 };

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
        var entityDTO = new AlbumDTO { Id = 1, Name = "Egg", ArtistId = 1 };

        // Act
        var result = service.Update("1", entityDTO).Result;
        var dto = result.Value;

        var result2 = service.Get("1", 1).Result;
        var dto2 = result2.Value;

        // Assert
        var expected = new AlbumDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Album", dto.Type);
        Assert.Equal(expected.Id, dto2.Id);
        Assert.Equal(expected.Name, dto2.Name);
        Assert.Equal(expected.Starred, dto2.Starred);
        Assert.Equal(expected.Notes, dto2.Notes);
        Assert.Equal("Album", dto2.Type);
    }

    [Fact]
    public void UpdateWithInvalidIdReturnsNull()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 25, Name = "Egg" };

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
        var entityDTO = new AlbumDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("5", entityDTO).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
    }

    [Fact]
    public void UpdateToArtistThatIsNotOwnedReturnsNull()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 1, Name = "Egg", ArtistId = 4 };

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
        var initialNumAlbums = service.GetAll("1").Result.Count();
        var result = service.Delete("1", 1).Result;

        var afterNumAlbums = service.GetAll("1").Result.Count();
        var deletedAlbums = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedAlbums.Value);
        Assert.Equal(initialNumAlbums - 1, afterNumAlbums);
        Assert.True(result.Success);
    }
    [Fact]
    public void CanNotDeleteAWithInvalidId()
    {
        var result = service.Delete("1", 5).Result;
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
