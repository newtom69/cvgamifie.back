using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITableTrainingCourseTrainerBusiness
    {
        TableTrainingCourseTrainer Create(TableTrainingCourseTrainer tableTrainingCourseStudent);
        TableTrainingCourseTrainer Read(int id);
        IEnumerable<TableTrainingCourseTrainer> List();
        bool Update(TableTrainingCourseTrainer tableTrainingCourseTrainerToUpdate);
        bool Delete(int id);
    }
}
