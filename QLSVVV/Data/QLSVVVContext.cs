using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLSVVV.Models;

namespace QLSVVV.Data
{
    public class QLSVVVContext : DbContext
    {
        public QLSVVVContext(DbContextOptions<QLSVVVContext> options)
            : base(options)
        {

        }

        public DbSet<QLSVVV.Models.User> User { get; set; } = default!;

        public DbSet<QLSVVV.Models.Student>? Student { get; set; }

        public DbSet<QLSVVV.Models.Teacher>? Teacher { get; set; }

        public DbSet<QLSVVV.Models.Course>? Course { get; set; }

        public DbSet<QLSVVV.Models.Class>? Class { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudent)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudent)
                .HasForeignKey(cs => cs.StudentId);
        }
        
    }
}
