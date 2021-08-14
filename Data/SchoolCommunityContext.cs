using Assignment2NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2NET.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {
        }

        public DbSet<Student> Students
        {
            get;
            set;
        }
        public DbSet<Community> Communities
        {
            get;
            set;
        }
        public DbSet<CommunityMembership> CommunityMemberships
        {
            get;
            set;
        }
        public DbSet<Advertisement> Advertisements 
        {
            get; 
            set; 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Community>().ToTable("Communities");
            modelBuilder.Entity<CommunityMembership>().ToTable("CommunityMemberships");
            modelBuilder.Entity<Advertisement>().ToTable("Advertisements");
            modelBuilder.Entity<CommunityMembership>().HasKey(c => new { c.StudentID, c.CommunityID });

        }
    }
}
