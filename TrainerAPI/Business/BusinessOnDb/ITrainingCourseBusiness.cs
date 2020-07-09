using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITrainingCourseBusiness
    {
        TrainingCourse Create(TrainingCourse trainingCourse);
        TrainingCourse Read(int id);
        IEnumerable<TrainingCourse> List();
        bool Update(TrainingCourse trainingCourseToUpdate);
        bool Delete(int id);
    }
}
