namespace Data.Model
{
    public class Stage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestId { get; set; }

        public virtual Quest Quest { get; set; }
    }
}
