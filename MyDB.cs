using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOD;
using USER;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MySql.EntityFrameworkCore.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Myd
{
    public class MyDB:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Good> Goods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置user和的一对一关系
            modelBuilder.Entity<User>()
                .HasMany(d => d.goods) // Order_Detail有多个Node
                .WithOne(n => n.user) // Node关联一个Order_Detail
                .HasForeignKey(n => n.UserId) // 外键
                .OnDelete(DeleteBehavior.Cascade); // 级联删除

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=final;user=root;password=744735631qaws;");
        }
    }
   

}

