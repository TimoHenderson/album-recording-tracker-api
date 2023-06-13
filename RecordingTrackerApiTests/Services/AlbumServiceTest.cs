using Microsoft.AspNetCore.Mvc.Diagnostics;
using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

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

        // Assert
        var expected = new AlbumDTO { Id = 1, Name = "Album 1", Starred = false, Notes = "", ArtistId = 1 };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.ArtistId, result.ArtistId);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Album", result.Type);
    }

    [Fact]
    public void CanCreate()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 0, Name = "Timothy", ArtistId = 1 };

        // Act
        var result = service.Create("1", entityDTO).Result;

        // Assert
        var expected = new AlbumDTO { Name = "Timothy", Starred = false, Notes = "", ArtistId = 1 };

        Assert.NotEqual(0, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Album", result.Type);
    }

    [Fact]
    public void CanNotCreateIfArtistIsNotOwnedByUser()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 0, Name = "Timothy", ArtistId = 4 };

        // Act
        var result = service.Create("1", entityDTO).Result;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CanUpdate()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 1, Name = "Egg", ArtistId = 1 };

        // Act
        var result = service.Update("1", entityDTO).Result;

        var result2 = service.Get("1", 1).Result;

        // Assert
        var expected = new AlbumDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Album", result.Type);
        Assert.Equal(expected.Id, result2.Id);
        Assert.Equal(expected.Name, result2.Name);
        Assert.Equal(expected.Starred, result2.Starred);
        Assert.Equal(expected.Notes, result2.Notes);
        Assert.Equal("Album", result2.Type);
    }

    [Fact]
    public void UpdateWithInvalidIdReturnsNull()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 25, Name = "Egg" };

        // Act
        var result = service.Update("1", entityDTO).Result;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateWithInvalidUserIdReturnsNull()
    {
        // Arrange
        var entityDTO = new AlbumDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("5", entityDTO).Result;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CanDelete()
    {
        // Act
        var initialNumAlbums = service.GetAll("1").Result.Count();
        service.Delete("1", 1);

        var afterNumAlbums = service.GetAll("1").Result.Count();
        var deletedAlbums = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedAlbums);
        Assert.Equal(initialNumAlbums - 1, afterNumAlbums);
    }
    [Fact]
    public void CanNotDeleteAWithInvalidId()
    {

        var result = service.Delete("1", 5);

        Assert.Null(result.Result);

    }
    [Fact]
    public void CanNotDeleteWithInvalidUserId()
    {
        var result = service.Delete("5", 1);
        Assert.Null(result.Result);
    }
}
