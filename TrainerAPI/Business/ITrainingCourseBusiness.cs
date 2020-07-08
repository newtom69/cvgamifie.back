using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
