using FrogChatModel.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatDAL
{
    public class FrogChatDbContext : IdentityDbContext
    {
        public FrogChatDbContext(DbContextOptions<FrogChatDbContext> options)
            : base(options)
        {

        }

        #region previous User Role Table 
        //public virtual DbSet<TblUser> Users { get; set; }
        //public virtual DbSet<TblRole> Roles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //here seed data

        //    //Seed Departments Table
        //    modelBuilder.Entity<TblRole>().HasData(
        //        new TblRole { Id = 1, Name = "Admin" });
        //    modelBuilder.Entity<TblRole>().HasData(
        //         new TblRole { Id = 2, Name = "Manager" });
        //    modelBuilder.Entity<TblRole>().HasData(
        //        new TblRole { Id = 3, Name = "User" });

        //    //Seed Employee Table
        //    modelBuilder.Entity<TblUser>().HasData(new TblUser
        //    {
        //        Id= 1,
        //        Name = "Frogora",
        //        Email = "rr4428310@gmail.com",
        //        Identifier = "115412363636721186069",
        //        PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5r5h2endXrYUH1Ad9moiIPDxZy6bYoI5ppRO8mqg=s96-c",
        //        RoleId = 1
        //    });

        //    modelBuilder.Entity<TblUser>().HasData(new TblUser
        //    {
        //        Id=2,
        //        Name = "nanu naka",
        //        Email = "my4lol78695@gmail.com",
        //        Identifier = "109111229606383522361",
        //        PhotoPath = @"https://lh3.googleusercontent.com/a/AEdFTp5jcsydwm4AsQRoEruEyjnu9ic2B8vX1wc3zBC7=s96-c",
        //        RoleId = 3
        //    });
        //}
        #endregion
    }
}
