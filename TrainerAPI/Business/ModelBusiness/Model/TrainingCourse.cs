using System.Collections.Generic;
using Data.Model;

namespace TrainerAPI.Business.Model
{
    public class TrainingCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quest> Quests { get; set; }
        public User Owner { get; set; }
        public List<User> Trainers { get; set; }
        public List<User> Students { get; set; }
    }
}
