using Microsoft.EntityFrameworkCore;
using Resume.API.Models;

namespace Resume.API.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserResume> UserResumes { get; set; }
    }
}
