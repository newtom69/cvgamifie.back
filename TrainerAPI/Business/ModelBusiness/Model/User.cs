using Data.Model;

namespace TrainerAPI.Business.Model
{
    public class User
    {
        private TableTrainingCourseStudent tableTrainingCourseStudent;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentXp { get; set; }

        public User()
        {
        }

        public User(TableUser tableUser)
        {
            Id = tableUser.Id;
            FirstName = "";
            LastName = tableUser.UserName;
            StudentXp = tableUser.StudentXP;
        }
    }
}