using System.Collections.Generic;
using TrainerAPI.Business.Model;

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
