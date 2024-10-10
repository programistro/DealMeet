using DealMeet.Core;
using Microsoft.EntityFrameworkCore;

namespace DealMeet.Data;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public UserDbContext()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=users.db");
    }
}