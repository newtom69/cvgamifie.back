using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    /// <summary>
    /// Classe métier de la gestion des formations (TrainingCourse)
    /// </summary>
    public class TrainingCourseBusiness : ITrainingCourseBusiness
    {
        private readonly DefaultContext _defaultContext;

        public TrainingCourseBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public TrainingCourse Create(TrainingCourse trainingCourse)
        {
            var addResult = _defaultContext.TrainingCourses.Add(trainingCourse);
            int saveResult;
            try
            {
                saveResult = _defaultContext.SaveChanges();
            }
            catch
            {
                return null;
            }
            return saveResult == 1 ? addResult.Entity : null;
        }

        public TrainingCourse Read(int id)
        {
            var trainingCourse = _defaultContext.TrainingCourses.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return trainingCourse;
        }

        public IEnumerable<TrainingCourse> List()
        {
            var trainingCourses = _defaultContext.TrainingCourses.AsNoTracking();
            return trainingCourses;
        }

        public bool Update(TrainingCourse trainingCourseToUpdate)
        {
            var trainingCourse = _defaultContext.TrainingCourses.AsNoTracking().FirstOrDefault(x => x.Id == trainingCourseToUpdate.Id);
            if (trainingCourse == null)
                return false;

            _defaultContext.TrainingCourses.Update(trainingCourseToUpdate);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public bool Delete(int id)
        {
            var trainingCourse = _defaultContext.TrainingCourses.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (trainingCourse == null)
                return false;

            _defaultContext.TrainingCourses.Remove(trainingCourse);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }
    }
}
