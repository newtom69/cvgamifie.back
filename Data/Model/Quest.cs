
namespace Data.Model
{
    /// <summary>
    /// les quêtes des formations
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// id de la quête (incrémental)
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Nom de la quête
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// id de la formation de cette quête
        /// </summary>
        public int TrainingCourseId { get; set; }

        /// <summary>
        /// id de la quête principale (le cas échéant)
        /// </summary>
        public int? MainQuestId { get; set; }

        /// <summary>
        /// numéro de la quête dans la formation
        /// </summary>
        public int Number { get; set; }


        public TrainingCourse TrainingCourse { get; set; }
        public Quest MainQuest { get; set; }
    }
}
