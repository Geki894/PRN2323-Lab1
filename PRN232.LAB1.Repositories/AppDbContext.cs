using Microsoft.EntityFrameworkCore;
using PRN232.LAB1.Repositories.Models;

namespace PRN232.LAB1.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Semester_Tam> Semesters { get; set; }
        public DbSet<Subject_Tam> Subjects { get; set; }
        public DbSet<Course_Tam> Courses { get; set; }
        public DbSet<Student_Tam> Students { get; set; }
        public DbSet<Enrollment_Tam> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
