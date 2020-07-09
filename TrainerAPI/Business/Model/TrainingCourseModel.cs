using System.Collections.Generic;

namespace TrainerAPI.Business.Model
{
    public class TrainingCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestModel> Quests { get; set; }
        public UserModel Owner { get; set; }
        public List<UserModel> Trainers { get; set; }
        public List<UserModel> Students { get; set; }



    }
}
