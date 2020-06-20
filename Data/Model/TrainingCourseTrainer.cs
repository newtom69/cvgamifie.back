namespace Data.Model
{
    /// <summary>
    /// le/les formateurs d'une formation 
    /// </summary>
    public class TrainingCourseTrainer
    {
        public int Id { get; set; }
        public int TrainingCourseId { get; set; }
        public int TrainerId { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }
        public virtual User Trainer { get; set; }
    }
}
