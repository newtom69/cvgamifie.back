using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Context : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<TrainingCourse> TrainingCourses { get; set; }
        public DbSet<TrainingCourseTrainer> TrainingCourseTrainers { get; set; }
        public DbSet<TrainingCourseStudent> TrainingCourseStudents { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Stage> Stages { get; set; }



        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Method intentionally left empty.
        }
    }
}
