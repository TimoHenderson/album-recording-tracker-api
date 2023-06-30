using RecordingTrackerApi.Models.RecordingEntities.DTOs;
using RecordingTrackerApi.Services;
using RecordingTrackerApi.Results;

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
        var dto = result.Value;

        // Assert
        var expected = new ArtistDTO { Id = 1, Name = "Artist 1", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Artist", dto.Type);
    }

    [Fact]
    public void CanCreateArtist()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 0, Name = "Timothy" };

        // Act
        var result = service.Create("1", artist).Result;
        var dto = result.Value;

        // Assert
        var expected = new ArtistDTO { Name = "Timothy", Starred = false, Notes = "" };

        Assert.NotEqual(0, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Artist", dto.Type);
        Assert.True(result.Success);
    }

    [Fact]
    public void CanUpdateArtist()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("1", artist).Result;
        var dto = result.Value;

        var result2 = service.Get("1", 1).Result;
        var dto2 = result2.Value;

        // Assert
        var expected = new ArtistDTO { Id = 1, Name = "Egg", Starred = false, Notes = "" };

        Assert.Equal(expected.Id, dto.Id);
        Assert.Equal(expected.Name, dto.Name);
        Assert.Equal(expected.Starred, dto.Starred);
        Assert.Equal(expected.Notes, dto.Notes);
        Assert.Equal("Artist", dto.Type);
        Assert.Equal(expected.Id, dto2.Id);
        Assert.Equal(expected.Name, dto2.Name);
        Assert.Equal(expected.Starred, dto2.Starred);
        Assert.Equal(expected.Notes, dto2.Notes);
        Assert.Equal("Artist", dto2.Type);
        Assert.True(result.Success);
    }

    [Fact]
    public void UpdateArtistWithInvalidIdReturnsNull()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 25, Name = "Egg" };

        // Act
        var result = service.Update("1", artist).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
        Assert.Equal("Not found", result.ErrorMessage);
    }

    [Fact]
    public void UpdateArtistWithInvalidUserIdReturnsNull()
    {
        // Arrange
        var artist = new ArtistDTO { Id = 1, Name = "Egg" };

        // Act
        var result = service.Update("5", artist).Result;

        // Assert
        Assert.Null(result.Value);
        Assert.False(result.Success);
        Assert.Equal("Not owned by user", result.ErrorMessage);
    }

    [Fact]
    public void CanDeleteArtist()
    {
        // Act
        var initialNumArtists = service.GetAll("1").Result.Count();
        var result = service.Delete("1", 1).Result;

        var afterNumArtists = service.GetAll("1").Result.Count();
        var deletedArtists = service.Get("1", 1).Result;
        // Assert
        Assert.Null(deletedArtists.Value);
        Assert.Equal(initialNumArtists - 1, afterNumArtists);
        Assert.True(result.Success);
    }

    [Fact]
    public void CanNotDeleteArtiststWithInvalidId()
    {
        var result = service.Delete("1", 5).Result;

        Assert.Null(result.Value);
        Assert.False(result.Success);
        Assert.Equal("Not found", result.ErrorMessage);
    }

    [Fact]
    public void CanNotDeleteArtiststWithInvalidUserId()
    {
        var result = service.Delete("5", 1).Result;
        Assert.Null(result.Value);
        Assert.False(result.Success);
        Assert.Equal("Not owned by user", result.ErrorMessage);
    }
}
