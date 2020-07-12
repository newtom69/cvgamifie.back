using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    public class TableTrainingCourseStudentBusiness : ITableTrainingCourseStudentBusiness
    {
        private readonly DefaultContext _defaultContext;

        public TableTrainingCourseStudentBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public TableTrainingCourseStudent Create(TableTrainingCourseStudent tableTrainingCourseStudent)
        {
            var addResult = _defaultContext.TrainingCourseStudents.Add(tableTrainingCourseStudent);
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

        public TableTrainingCourseStudent Read(int id)
        {
            var trainingCourseStudent = _defaultContext.TrainingCourseStudents.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return trainingCourseStudent;
        }

        public IEnumerable<TableTrainingCourseStudent> List()
        {
            var trainingCourseStudents = _defaultContext.TrainingCourseStudents.AsNoTracking();
            return trainingCourseStudents;
        }

        public bool Update(TableTrainingCourseStudent tableTrainingCourseStudentToUpdate)
        {
            var trainingCourseStudent = _defaultContext.TrainingCourseStudents.AsNoTracking().FirstOrDefault(x => x.Id == tableTrainingCourseStudentToUpdate.Id);
            if (trainingCourseStudent == null)
                return false;

            _defaultContext.TrainingCourseStudents.Update(tableTrainingCourseStudentToUpdate);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public bool Delete(int id)
        {
            var trainingCourseStudent = _defaultContext.TrainingCourseStudents.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (trainingCourseStudent == null)
                return false;

            _defaultContext.TrainingCourseStudents.Remove(trainingCourseStudent);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }
    }
}
