using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    /// <summary>
    /// Classe métier de la gestion des formations (TableTrainingCourse)
    /// </summary>
    public class TableTrainingCourseBusiness : ITableTrainingCourseBusiness
    {
        private readonly DefaultContext _defaultContext;

        public TableTrainingCourseBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public TableTrainingCourse Create(TableTrainingCourse tableTrainingCourse)
        {
            var addResult = _defaultContext.TrainingCourses.Add(tableTrainingCourse);
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

        public TableTrainingCourse Read(int id)
        {
            var trainingCourse = _defaultContext.TrainingCourses.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return trainingCourse;
        }

        public IEnumerable<TableTrainingCourse> List()
        {
            var trainingCourses = _defaultContext.TrainingCourses.AsNoTracking();
            return trainingCourses;
        }

        public bool Update(TableTrainingCourse tableTrainingCourseToUpdate)
        {
            var trainingCourse = _defaultContext.TrainingCourses.AsNoTracking().FirstOrDefault(x => x.Id == tableTrainingCourseToUpdate.Id);
            if (trainingCourse == null)
                return false;

            _defaultContext.TrainingCourses.Update(tableTrainingCourseToUpdate);
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
