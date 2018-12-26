using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using WebApiNetCore.Models.MAINTE;

namespace WebApiNetCore.DataContext
{
    public class CATEGORY_CONTEXT: DbContext
    {
        public CATEGORY_CONTEXT (DbContextOptions<CATEGORY_CONTEXT> options)
            : base(options)
        {
        }

        public DbSet<WebApiNetCore.Models.MAINTE.CATEGORY> CATEGORY { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORY>()
                .HasKey(mainte => new { mainte.Id });
        } 
    }
}