using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }      

        // Will reflect the name of our database table, with columns matching whatever properties we've created for our Word class
        public DbSet<Word> Words { get; set; }
    }
}