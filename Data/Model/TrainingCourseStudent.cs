namespace Data.Model
{
    /// <summary>
    /// Les stagiaires d'une formation
    /// </summary>
    public class TrainingCourseStudent
    {
        public int Id { get; set; }
        public int TrainingCourseId { get; set; }
        public int StudentId { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }
        public virtual User Student { get; set; }
    }
}
