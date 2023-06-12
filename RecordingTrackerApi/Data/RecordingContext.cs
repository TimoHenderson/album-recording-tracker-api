using Microsoft.EntityFrameworkCore;
using RecordingTrackerApi.Models.RecordingEntities;

namespace RecordingTrackerApi.Data;

public class RecordingContext : DbContext
{
    public RecordingContext(DbContextOptions<RecordingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists => Set<Artist>();
    public virtual DbSet<Album> Albums => Set<Album>();
    public virtual DbSet<Song> Songs => Set<Song>();
    public virtual DbSet<Part> Parts => Set<Part>();
    public virtual DbSet<Instrument> Instruments => Set<Instrument>();
}