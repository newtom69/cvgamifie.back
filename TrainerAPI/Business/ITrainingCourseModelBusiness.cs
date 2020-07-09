using System.Collections.Generic;
using TrainerAPI.Business.Model;

namespace TrainerAPI.Business
{
    public interface ITrainingCourseModelBusiness
    {
        TrainingCourseModel Create(TrainingCourseModel trainingCourse);
        TrainingCourseModel Read(int id);
        IEnumerable<TrainingCourseModel> List();
        bool Update(TrainingCourseModel trainingCourseToUpdate);
        bool Delete(int id);
    }
}
