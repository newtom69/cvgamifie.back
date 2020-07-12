namespace Data.Model
{
    /// <summary>
    /// Les stagiaires d'une formation (jointure TableTrainingCourse / TableUser)
    /// </summary>
    public class TableTrainingCourseStudent
    {
        /// <summary>
        /// id (incrémental)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// id de la formation
        /// </summary>
        public int TrainingCourseId { get; set; }

        /// <summary>
        /// id de l'utilisateur stagiaire de cette jointure
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// point d'expérience du stagiaire sur cette formation
        /// </summary>
        public int StudentXp { get; set; }


        public virtual TableTrainingCourse TrainingCourse { get; set; }
        public virtual TableUser Student { get; set; }
    }
}
