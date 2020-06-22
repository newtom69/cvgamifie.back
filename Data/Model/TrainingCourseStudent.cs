namespace Data.Model
{
    /// <summary>
    /// Les stagiaires d'une formation (jointure TrainingCourse / User)
    /// </summary>
    public class TrainingCourseStudent
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
        public int StudentXP { get; set; }


        public virtual TrainingCourse TrainingCourse { get; set; }
        public virtual User Student { get; set; }
    }
}
