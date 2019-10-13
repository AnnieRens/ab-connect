using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess
{
    public class DbLogContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(@"Server=DESKTOP-K1IJ400\LOCAL;Database=MessageLog;Trusted_Connection=True;");
        }
    }
}
