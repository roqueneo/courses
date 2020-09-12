using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions contextOptions) 
            : base(contextOptions) 
        { }

        public DbSet<Course> Course { get; set; }         
        public DbSet<Price> Price { get; set; } 
        public DbSet<Comment> Comment { get; set; } 
        public DbSet<Instructor> Instructor { get; set; } 
        public DbSet<CourseInstructor> CourseInstructor { get; set; }         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new {ci.InstructorId, ci.CourseId});
        }

   }
}