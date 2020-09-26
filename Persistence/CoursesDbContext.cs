using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CoursesDbContext : IdentityDbContext<User>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new {ci.InstructorId, ci.CourseId});
        }

   }
}