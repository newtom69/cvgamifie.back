using Data.Model;

namespace TrainerAPI.Business.Model
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public Quest()
        {
        }

        public Quest(TableQuest tableQuest)
        {
            Id = tableQuest.Id;
            Name = tableQuest.Name;
            Number = tableQuest.Number;
        }
    }
}
