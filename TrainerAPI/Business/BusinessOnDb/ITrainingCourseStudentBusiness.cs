using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITrainingCourseStudentBusiness
    {
        TrainingCourseStudent Create(TrainingCourseStudent trainingCourseStudent);
        TrainingCourseStudent Read(int id);
        IEnumerable<TrainingCourseStudent> List();
        bool Update(TrainingCourseStudent trainingCourseStudentToUpdate);
        bool Delete(int id);
    }
}
