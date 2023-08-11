using BlockingUsers.Modules;
using Microsoft.EntityFrameworkCore;

namespace Blocking.Context
{
    public class AuthContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public AuthContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Users> Users { get; set; }
    }

}
