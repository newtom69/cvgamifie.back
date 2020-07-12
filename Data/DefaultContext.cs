using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// les différentes tables de la base de données de l'appli
    /// </summary>
    public class DefaultContext : IdentityDbContext<TableUser, IdentityRole<int>, int>
    {
        public DbSet<TableTrainingCourse> TrainingCourses { get; set; }
        public DbSet<TableUser> Users { get; set; }
        public DbSet<TableTrainingCourseTrainer> TrainingCourseTrainers { get; set; }
        public DbSet<TableTrainingCourseStudent> TrainingCourseStudents { get; set; }
        public DbSet<TableQuest> Quests { get; set; }
        public DbSet<TableStage> Stages { get; set; }



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
