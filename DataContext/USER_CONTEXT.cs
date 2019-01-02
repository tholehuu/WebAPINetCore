using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using WebApiNetCore.Models.User;

namespace WebApiNetCore.DataContext
{
    public class  USER_CONTEXT : DbContext
    {
        public USER_CONTEXT (DbContextOptions<USER_CONTEXT> options): base(options){

        }

        public DbSet<USER> User { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<USER>().HasKey(user => new {user.Id} );
        }
    }
}