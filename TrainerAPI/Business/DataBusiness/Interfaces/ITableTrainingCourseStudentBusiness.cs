using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITableTrainingCourseStudentBusiness
    {
        TableTrainingCourseStudent Create(TableTrainingCourseStudent tableTrainingCourseStudent);
        TableTrainingCourseStudent Read(int id);
        IEnumerable<TableTrainingCourseStudent> List();
        bool Update(TableTrainingCourseStudent tableTrainingCourseStudentToUpdate);
        bool Delete(int id);
    }
}
