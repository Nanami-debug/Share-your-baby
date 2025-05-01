using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOD;
using USER;
using ORDER;    
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MySql.EntityFrameworkCore.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Myd
{
    public class MyDB:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Good> Goods { get; set; }//user发布的商品
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// 多对多：用户收藏商品
            modelBuilder.Entity<User>()
                .HasMany(u => u.stared)
                .WithMany(g => g.StarredBy)
                .UsingEntity(j => j.ToTable("UserStarredGoods"));

            // 一对一：Good 和 Order
            modelBuilder.Entity<Good>()
                .HasOne(g => g.order)
                .WithOne(o => o.good)
                .HasForeignKey<Order>(o => o.goodId)
                .OnDelete(DeleteBehavior.SetNull);

            // 一对多：User -> Goods
            modelBuilder.Entity<User>()
                .HasMany(u => u.goods)
                .WithOne(g => g.user)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 一对多：User -> BuyOrders
            modelBuilder.Entity<User>()
                .HasMany(u => u.buy_orders)
                .WithOne(o => o.buyer)
                .HasForeignKey(o => o.buyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 一对多：User -> SellOrders
            modelBuilder.Entity<User>()
                .HasMany(u => u.sell_orders)
                .WithOne(o => o.seller)
                .HasForeignKey(o => o.sellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=homework;user=root;password=744735631qaws;");
        }
    }
   

}

