namespace Data.Model
{
    /// <summary>
    /// le/les formateurs d'une formation (jointure TrainingCourse / User)
    /// </summary>
    public class TrainingCourseTrainer
    {
        /// <summary>
        /// id (incrémental)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  id de la formation
        /// </summary>
        public int TrainingCourseId { get; set; }
        
        /// <summary>
        /// id de l'utilisateur formateur
        /// </summary>
        public int TrainerId { get; set; }


        public virtual TrainingCourse TrainingCourse { get; set; }
        public virtual User Trainer { get; set; }
    }
}
