using Microsoft.EntityFrameworkCore;
using DealMeet.Core;

namespace DealMeet.Data;

public class ChatDbContext : DbContext
{
    public DbSet<Message> Messages => Set<Message>();
    
    public ChatDbContext()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=chat.db");
    }
}