using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;

namespace RecordingTrackerApiTests;


public class ArtistServiceTest
{
    private readonly ArtistsService service;

    public ArtistServiceTest()
    {
        var context = new InMemoryDatabaseFixture().CreateContext();
        service = new ArtistsService(context);
    }

    [Theory]
    [InlineData("1", 3)]
    [InlineData("2", 1)]
    public void CanGetAllArtists(string userId, int expected)
    {
        // Act
        var artists = service.GetAll(userId).Result;

        // Assert
        Assert.Equal(expected, artists.Count());
    }

    [Fact]
    public void CanGetArtistById()
    {
        // Act
        var result = service.Get("1", 1).Result;

        // Assert
        var expected = new ArtistDTO { Id = 1, Name = "Artist 1", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Artist", result.Type);
    }

    [Fact]
    public void CanCreateArtist()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 0, Name = "Timothy" };

        // Act
        var result = service.Create("1", artist).Result;

        // Assert
        var expected = new ArtistDTO { Name = "Timothy", Starred = false, Notes = "" };

        Assert.NotEqual(0, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Artist", result.Type);
    }

    [Fact]
    public void CanUpdateArtist()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("1", artist).Result;

        var result2 = service.Get("1", 1).Result;

        // Assert
        var expected = new ArtistDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Starred, result.Starred);
        Assert.Equal(expected.Notes, result.Notes);
        Assert.Equal("Artist", result.Type);
        Assert.Equal(expected.Id, result2.Id);
        Assert.Equal(expected.Name, result2.Name);
        Assert.Equal(expected.Starred, result2.Starred);
        Assert.Equal(expected.Notes, result2.Notes);
        Assert.Equal("Artist", result2.Type);
    }

    [Fact]
    public void UpdateArtistWithInvalidIdReturnsNull()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 25, Name = "Egg" };

        // Act
        var result = service.Update("1", artist).Result;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateArtistWithInvalidUserIdReturnsNull()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("5", artist).Result;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CanDeleteArtist()
    {
        // Act
        var initialNumArtists = service.GetAll("1").Result.Count();
        service.Delete("1", 1);

        var afterNumArtists = service.GetAll("1").Result.Count();
        var deletedArtists = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedArtists);
        Assert.Equal(initialNumArtists - 1, afterNumArtists);
    }
    [Fact]
    public void CanNotDeleteArtiststWithInvalidId()
    {

        var result = service.Delete("1", 5);

        Assert.Null(result.Result);

    }
    [Fact]
    public void CanNotDeleteArtiststWithInvalidUserId()
    {
        var result = service.Delete("5", 1);
        Assert.Null(result.Result);
    }
}
