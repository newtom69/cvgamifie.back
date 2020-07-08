using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// les différentes tables de la base de données de l'appli
    /// </summary>
    public class DefaultContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<TrainingCourse> TrainingCourses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TrainingCourseTrainer> TrainingCourseTrainers { get; set; }
        public DbSet<TrainingCourseStudent> TrainingCourseStudents { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Stage> Stages { get; set; }



        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Method intentionally left empty.
        }

        // ReSharper disable once RedundantOverriddenMember
#pragma warning disable S1185 // Overriding members should do more than simply call the same member in the base class
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
#pragma warning restore S1185 // Overriding members should do more than simply call the same member in the base class



    }
}
