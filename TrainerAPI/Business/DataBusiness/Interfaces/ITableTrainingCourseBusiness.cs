using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITableTrainingCourseBusiness
    {
        TableTrainingCourse Create(TableTrainingCourse tableTrainingCourse);
        TableTrainingCourse Read(int id);
        IEnumerable<TableTrainingCourse> List();
        bool Update(TableTrainingCourse tableTrainingCourseToUpdate);
        bool Delete(int id);
    }
}
