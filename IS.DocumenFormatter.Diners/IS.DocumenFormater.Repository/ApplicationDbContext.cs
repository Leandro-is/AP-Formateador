using IS.DocumenFormater.Repository.Domain;
using Microsoft.EntityFrameworkCore;

namespace IS.DocumenFormater.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TransaccionalDocumentFormater> TransaccionalDocumentFormaters { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}