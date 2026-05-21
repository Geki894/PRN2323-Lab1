using Bogus;
using Microsoft.EntityFrameworkCore;
using PRN232.LAB1.Repositories.Models;
using System;
using System.Collections.Generic;

namespace PRN232.LAB1.Repositories
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Randomizer.Seed = new Random(123456);

            // 1. Seed Semesters (5)
            var semesters = new List<Semester_Tam>
            {
                new Semester_Tam { SemesterId = 1, Name = "Fall 2025", StartDate = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc) },
                new Semester_Tam { SemesterId = 2, Name = "Spring 2026", StartDate = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc) },
                new Semester_Tam { SemesterId = 3, Name = "Summer 2026", StartDate = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc) },
                new Semester_Tam { SemesterId = 4, Name = "Fall 2026", StartDate = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc) },
                new Semester_Tam { SemesterId = 5, Name = "Spring 2027", StartDate = new DateTime(2027, 1, 15, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2027, 5, 15, 0, 0, 0, DateTimeKind.Utc) }
            };
            modelBuilder.Entity<Semester_Tam>().HasData(semesters);

            // 2. Seed Subjects (10)
            var subjectIds = 1;
            var subjectFaker = new Faker<Subject_Tam>()
                .RuleFor(s => s.SubjectId, f => subjectIds++)
                .RuleFor(s => s.Name, f => f.Commerce.Department() + " " + f.Random.AlphaNumeric(3).ToUpper())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.Credits, f => f.Random.Int(2, 4));
            
            var subjects = subjectFaker.Generate(10);
            modelBuilder.Entity<Subject_Tam>().HasData(subjects);

            // 3. Seed Courses (20)
            var courseIds = 1;
            var courseFaker = new Faker<Course_Tam>()
                .RuleFor(c => c.CourseId, f => courseIds++)
                .RuleFor(c => c.Name, f => "Course " + f.Commerce.ProductName())
                .RuleFor(c => c.SemesterId, f => f.PickRandom(semesters).SemesterId)
                .RuleFor(c => c.SubjectId, f => f.PickRandom(subjects).SubjectId)
                .RuleFor(c => c.MaxStudents, f => f.Random.Int(20, 50));
            
            var courses = courseFaker.Generate(20);
            modelBuilder.Entity<Course_Tam>().HasData(courses);

            // 4. Seed Students (50)
            var studentIds = 1;
            var studentFaker = new Faker<Student_Tam>()
                .RuleFor(s => s.StudentId, f => studentIds++)
                .RuleFor(s => s.FullName, f => f.Name.FullName())
                .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.FullName))
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, DateTime.UtcNow.AddYears(-18)).ToUniversalTime());
            
            var students = studentFaker.Generate(50);
            modelBuilder.Entity<Student_Tam>().HasData(students);

            // 5. Seed Enrollments (500)
            var enrollmentIds = 1;
            var enrollmentFaker = new Faker<Enrollment_Tam>()
                .RuleFor(e => e.EnrollmentId, f => enrollmentIds++)
                .RuleFor(e => e.StudentId, f => f.PickRandom(students).StudentId)
                .RuleFor(e => e.CourseId, f => f.PickRandom(courses).CourseId)
                .RuleFor(e => e.EnrollDate, f => f.Date.Past(1).ToUniversalTime())
                .RuleFor(e => e.Status, f => f.PickRandom(new[] { "Active", "Completed", "Dropped" }));
            
            var enrollments = enrollmentFaker.Generate(500);
            modelBuilder.Entity<Enrollment_Tam>().HasData(enrollments);
        }
    }
}
