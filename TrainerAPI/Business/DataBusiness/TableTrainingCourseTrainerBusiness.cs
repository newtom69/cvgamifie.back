using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    public class TableTrainingCourseTrainerBusiness : ITableTrainingCourseTrainerBusiness
    {
        private readonly DefaultContext _defaultContext;

        public TableTrainingCourseTrainerBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public TableTrainingCourseTrainer Create(TableTrainingCourseTrainer tableTrainingCourseStudent)
        {
            var addResult = _defaultContext.TrainingCourseTrainers.Add(tableTrainingCourseStudent);
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

        public TableTrainingCourseTrainer Read(int id)
        {
            var trainingCourseTrainer = _defaultContext.TrainingCourseTrainers.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return trainingCourseTrainer;
        }

        public IEnumerable<TableTrainingCourseTrainer> List()
        {
            var trainingCourseTrainers = _defaultContext.TrainingCourseTrainers.AsNoTracking();
            return trainingCourseTrainers;
        }

        public bool Update(TableTrainingCourseTrainer tableTrainingCourseTrainerToUpdate)
        {
            var trainingCourseTrainer = _defaultContext.TrainingCourseTrainers.AsNoTracking().FirstOrDefault(x => x.Id == tableTrainingCourseTrainerToUpdate.Id);
            if (trainingCourseTrainer == null)
                return false;

            _defaultContext.TrainingCourseTrainers.Update(tableTrainingCourseTrainerToUpdate);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public bool Delete(int id)
        {
            var trainingCourseTrainer = _defaultContext.TrainingCourseTrainers.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (trainingCourseTrainer == null)
                return false;

            _defaultContext.TrainingCourseTrainers.Remove(trainingCourseTrainer);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }
    }
}
