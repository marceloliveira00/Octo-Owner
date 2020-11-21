using Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Data
{
    public class SqlContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }
    }
}

