using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICandidate.Model;

namespace APICandidate.Model
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Assertive>().ToTable("Assertive");
            modelBuilder.Entity<UpdateInfo>().ToTable("UpdateInfo");
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Assertive> Assertives { get; set; }
        public DbSet<UpdateInfo> UpdateInfo { get; set; }       
        public DbSet<APICandidate.Model.ProfileQuestion> ProfileQuestion { get; set; }
    }
}
