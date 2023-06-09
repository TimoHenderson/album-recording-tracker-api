using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Models.RecordingEntities;

namespace RecordingTrackerApi.Data;

public class RecordingContext : DbContext
{
    public RecordingContext(DbContextOptions<RecordingContext> options)
        : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<Instrument> Instruments => Set<Instrument>();
}