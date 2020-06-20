namespace Data.Model
{
    public class TrainingCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

    }
}
