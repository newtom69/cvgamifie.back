
namespace Data.Model
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrainingCourseId { get; set; }

        public TrainingCourse TrainingCourse { get; set; }
    }
}
