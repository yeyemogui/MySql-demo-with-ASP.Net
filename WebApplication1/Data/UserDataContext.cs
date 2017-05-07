using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace WebApplication1.Models
{
    public class UserDataContext : DbContext
    {
        public DbSet<User> Users { set; get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(@"Server=localhost;database=userinfo;uid=root;pwd=wuqing");
    }
}
