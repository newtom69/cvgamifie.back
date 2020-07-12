namespace Data.Model
{
    /// <summary>
    /// le/les formateurs d'une formation (jointure TableTrainingCourse / TableUser)
    /// </summary>
    public class TableTrainingCourseTrainer
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


        public virtual TableTrainingCourse TrainingCourse { get; set; }
        public virtual TableUser Trainer { get; set; }
    }
}
