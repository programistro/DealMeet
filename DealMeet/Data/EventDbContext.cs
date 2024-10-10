using DealMeet.Core;
using Microsoft.EntityFrameworkCore;

namespace DealMeet.Data;

public class EventDbContext : DbContext
{
    public DbSet<Event> Events { get; set; } = null!;

    public EventDbContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=event.db");
    }
}