using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Data.Sqlite;
using RecordingTrackerApi.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using RecordingTrackerApi.Models.RecordingEntities;
using System.Data.Common;

namespace RecordingTrackerApiTests;

[Collection("DatabaseCollection")]
public class InMemoryDatabaseFixture : IDisposable
{
    // private bool _disposed = false;
    // public RecordingContext Context { get; private set; }

    // public InMemoryDatabaseFixture()
    // {
    //     var options = new DbContextOptionsBuilder<RecordingContext>()
    //         .UseInMemoryDatabase(databaseName: "RecordingTrackerTestDatabase")
    //         .Options;

    //     Context = new RecordingContext(options);

    //     IList<Instrument> instruments = new List<Instrument>{
    //         new Instrument { Id = 1, Name = "Guitar" ,AspNetUserId = "1"},
    //         new Instrument { Id = 2, Name = "Bass" ,AspNetUserId = "1"},
    //         new Instrument { Id = 3, Name = "Drums",    AspNetUserId = "2" },
    //     };
    //     Context.Instruments.AddRange(instruments);
    //     Context.SaveChanges();
    // }

    // public void Dispose()
    // {
    //     Context = null;
    //     //Dispose(true);
    //     //GC.SuppressFinalize(this);
    // }

    // protected virtual void Dispose(bool disposing)
    // {
    //     if (!_disposed)
    //     {
    //         if (disposing)
    //         {
    //             // Dispose managed resources here
    //             this.Dispose();
    //         }

    //         // Dispose unmanaged resources here

    //         _disposed = true;
    //     }
    // }
    private readonly DbConnection _connection;
    private readonly DbContextOptions<RecordingContext> _contextOptions;

    #region ConstructorAndDispose
    public InMemoryDatabaseFixture()
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<RecordingContext>()
            .UseSqlite(_connection)
            .Options;

        // Create the schema and seed some data
        using var context = new RecordingContext(_contextOptions);


        if (context.Database.EnsureCreated())
        {
            //     using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            //     viewCommand.CommandText = @"
            // CREATE VIEW AllResources AS
            // SELECT Instrument
            // FROM Instruments;";
            //     viewCommand.ExecuteNonQuery();
        }

        SeedDatabase(context);
    }

    public RecordingContext CreateContext() => new(_contextOptions);

    public void Dispose() => _connection.Dispose();
    #endregion

    private static void SeedDatabase(RecordingContext context)
    {
        var userId = "1";
        var userId2 = "2";

        var guitarInstrumentUser1 = new Instrument { Name = "Guitar", AspNetUserId = userId };
        var drumsInstrumentUser1 = new Instrument { Name = "Drums", AspNetUserId = userId };
        var guitarInstrumentUser2 = new Instrument { Name = "Guitar", AspNetUserId = userId2 };
        context.Instruments.AddRange(guitarInstrumentUser1, drumsInstrumentUser1, guitarInstrumentUser2);
        context.SaveChanges();

        var guitarInstrument = new Instrument { Name = "Guitar" };
        var bassInstrument = new Instrument { Name = "Bass" };
        var drumsInstrument = new Instrument { Name = "Drums" };
        var tromboneInstrument = new Instrument { Name = "Trombone" };
        var keysInstrument = new Instrument { Name = "Keys" };

        var song1GuitarPart = new Part { Name = "Guitar Part", Instrument = guitarInstrument, Completion = 20, AspNetUserId = userId };
        var song1BassPart = new Part { Name = "Bass Part", Instrument = bassInstrument, Completion = 20, AspNetUserId = userId };
        var song1DrumsPart = new Part { Name = "Drums Part", Instrument = drumsInstrument, Completion = 20, AspNetUserId = userId };

        var song2GuitarPart = new Part { Name = "Guitar Part2", Instrument = guitarInstrument };
        var song2BassPart = new Part { Name = "Bass Part2", Instrument = bassInstrument };
        var song2DrumsPart = new Part { Name = "Drums Part2", Instrument = drumsInstrument };

        var song1 = new Song { Name = "Song 1", Parts = new List<Part> { song1GuitarPart, song1BassPart, song1DrumsPart }, AspNetUserId = userId };
        var song2 = new Song { Name = "Song 2", Parts = new List<Part> { song2GuitarPart, song2BassPart, song2DrumsPart }, AspNetUserId = userId };

        var album1 = new Album { Name = "Album 1", Songs = new List<Song> { song1, song2 }, AspNetUserId = userId };

        var artist1 = new Artist { Name = "Artist 1", Albums = new List<Album> { album1 }, AspNetUserId = userId };

        var song3GuitarPart = new Part { Name = "Guitar Part3", Instrument = guitarInstrument, Completion = 20, AspNetUserId = userId };
        var song3BassPart = new Part { Name = "Bass Part3", Instrument = bassInstrument, AspNetUserId = userId };
        var song3DrumsPart = new Part { Name = "Drums Part3", Instrument = drumsInstrument, AspNetUserId = userId };

        var song4GuitarPart = new Part { Name = "Guitar Part4", Instrument = guitarInstrument, AspNetUserId = userId };
        var song4BassPart = new Part { Name = "Bass Part4", Instrument = bassInstrument, AspNetUserId = userId };
        var song4DrumsPart = new Part { Name = "Drums Part4", Instrument = drumsInstrument, AspNetUserId = userId };

        var song3 = new Song { Name = "Song 3", Parts = new List<Part> { song3GuitarPart, song3BassPart, song3DrumsPart }, AspNetUserId = userId };
        var song4 = new Song { Name = "Song 4", Parts = new List<Part> { song4GuitarPart, song4BassPart, song4DrumsPart }, AspNetUserId = userId };

        var album2 = new Album { Name = "Album 2", Songs = new List<Song> { song3, song4 }, AspNetUserId = userId };

        var artist2 = new Artist { Name = "Artist 2", Albums = new List<Album> { album2 }, AspNetUserId = userId };

        var artist3 = new Artist { Name = "Artist 3", AspNetUserId = userId };

        var song5GuitarPart = new Part { Name = "Guitar Part5", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song5BassPart = new Part { Name = "Bass Part5", Instrument = bassInstrument, Completion = 60, AspNetUserId = userId2 };
        var song5DrumsPart = new Part { Name = "Drums Part5", Instrument = drumsInstrument, Completion = 40, AspNetUserId = userId2 };

        var song6GuitarPart = new Part { Name = "Guitar Part6", Instrument = guitarInstrument, Completion = 100, AspNetUserId = userId2 };
        var song6BassPart = new Part { Name = "Bass Part6", Instrument = bassInstrument, Completion = 20, AspNetUserId = userId2 };
        var song6DrumsPart = new Part { Name = "Drums Part6", Instrument = drumsInstrument, Completion = 60, AspNetUserId = userId2 };
        var song6TrombonePart = new Part { Name = "Trombone Part6", Instrument = tromboneInstrument, Completion = 100, AspNetUserId = userId2 };
        var song6KeysPart = new Part { Name = "Keys Part6", Instrument = keysInstrument, Completion = 100, AspNetUserId = userId2 };

        var song7GuitarPart = new Part { Name = "Guitar Part7", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song7BassPart = new Part { Name = "Bass Part7", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song7DrumsPart = new Part { Name = "Drums Part7", Instrument = drumsInstrument, Completion = 20, AspNetUserId = userId2 };
        var song7TrombonePart = new Part { Name = "Trombone Part7", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song7KeysPart = new Part { Name = "Keys Part7", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song5 = new Song { Name = "Clack, Graabes, and Renfrewshire", Parts = new List<Part> { song5GuitarPart, song5BassPart, song5DrumsPart }, AspNetUserId = userId2 };
        var song6 = new Song { Name = "Sexy(Stick Your Bum Out)", Parts = new List<Part> { song6GuitarPart, song6BassPart, song6DrumsPart, song6KeysPart, song6TrombonePart }, AspNetUserId = userId2 };
        var song7 = new Song { Name = "What's The Problem?", Parts = new List<Part> { song7GuitarPart, song7BassPart, song7DrumsPart, song7TrombonePart, song7KeysPart }, AspNetUserId = userId2 };

        var album3 = new Album { Name = "Are You Papylonian?", Songs = new List<Song> { song5, song6, song7 }, AspNetUserId = userId2 };

        var song8GuitarPart = new Part { Name = "Guitar Part8", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song8BassPart = new Part { Name = "Bass Part8", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song8DrumsPart = new Part { Name = "Drums Part8", Instrument = drumsInstrument, Completion = 100, AspNetUserId = userId2 };
        var song8TrombonePart = new Part { Name = "Trombone Part8", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song8KeysPart = new Part { Name = "Keys Part8", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song9GuitarPart = new Part { Name = "Guitar Part9", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song9BassPart = new Part { Name = "Bass Part9", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song9DrumsPart = new Part { Name = "Drums Part9", Instrument = drumsInstrument, Completion = 20, AspNetUserId = userId2 };
        var song9TrombonePart = new Part { Name = "Trombone Part9", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song9KeysPart = new Part { Name = "Keys Part9", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song10GuitarPart = new Part { Name = "Guitar Part10", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song10BassPart = new Part { Name = "Bass Part10", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song10DrumsPart = new Part { Name = "Drums Part10", Instrument = drumsInstrument, Completion = 20, AspNetUserId = userId2 };
        var song10TrombonePart = new Part { Name = "Trombone Part10", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song10KeysPart = new Part { Name = "Keys Part10", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song11GuitarPart = new Part { Name = "Guitar Part11", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song11BassPart = new Part { Name = "Bass Part11", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song11DrumsPart = new Part { Name = "Drums Part11", Instrument = drumsInstrument, Completion = 20 };
        var song11TrombonePart = new Part { Name = "Trombone Part11", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song11KeysPart = new Part { Name = "Keys Part11", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song12GuitarPart = new Part { Name = "Guitar Part12", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song12BassPart = new Part { Name = "Bass Part12", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song12DrumsPart = new Part { Name = "Drums Part12", Instrument = drumsInstrument, Completion = 20, AspNetUserId = userId2 };
        var song12TrombonePart = new Part { Name = "Trombone Part12", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song12KeysPart = new Part { Name = "Keys Part12", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song13GuitarPart = new Part { Name = "Guitar Part13", Instrument = guitarInstrument, Completion = 80, AspNetUserId = userId2 };
        var song13BassPart = new Part { Name = "Bass Part13", Instrument = bassInstrument, Completion = 40, AspNetUserId = userId2 };
        var song13DrumsPart = new Part { Name = "Drums Part13", Instrument = drumsInstrument, Completion = 20, AspNetUserId = userId2 };
        var song13TrombonePart = new Part { Name = "Trombone Part13", Instrument = tromboneInstrument, Completion = 40, AspNetUserId = userId2 };
        var song13KeysPart = new Part { Name = "Keys Part13", Instrument = keysInstrument, Completion = 20, AspNetUserId = userId2 };

        var song8 = new Song { Name = "Old Tired Cold Bones", Parts = new List<Part> { song8GuitarPart, song8BassPart, song8DrumsPart, song8TrombonePart, song8KeysPart }, AspNetUserId = userId2 };
        var song9 = new Song { Name = "Egyptian Space Pirates", Parts = new List<Part> { song9GuitarPart, song9BassPart, song9DrumsPart, song9TrombonePart, song9KeysPart }, AspNetUserId = userId2 };
        var song10 = new Song { Name = "Late Night Sneaky Mama", Parts = new List<Part> { song10GuitarPart, song10BassPart, song10DrumsPart, song10TrombonePart, song10KeysPart }, AspNetUserId = userId2 };
        var song11 = new Song { Name = "The Ballad of the 3rd Arm", Parts = new List<Part> { song11GuitarPart, song11BassPart, song11DrumsPart, song11TrombonePart, song11KeysPart }, AspNetUserId = userId2 };
        var song12 = new Song { Name = "Grap", Parts = new List<Part> { song12GuitarPart, song12BassPart, song12DrumsPart, song12TrombonePart, song12KeysPart }, AspNetUserId = userId2 };
        var song13 = new Song { Name = "Scientific Facts", Parts = new List<Part> { song13GuitarPart, song13BassPart, song13DrumsPart, song13TrombonePart, song13KeysPart }, AspNetUserId = userId2 };

        var album4 = new Album { Name = "Paradigm Lost", Songs = new List<Song> { song8, song9, song10, song11, song12, song13 }, AspNetUserId = userId2 };
        var artist4 = new Artist { Name = "Bar Room Crawl", Albums = new List<Album> { album3, album4 }, AspNetUserId = userId2 };


        var artists = new Artist[] { artist1, artist2, artist3, artist4 };
        context.Artists.AddRange(artists);
        context.SaveChanges();
    }
}

