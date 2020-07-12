using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface ITableQuestBusiness
    {
        TableQuest Create(TableQuest tableQuest);
        TableQuest Read(int id);
        IEnumerable<TableQuest> List();
        IEnumerable<TableQuest> QuestsList(TableTrainingCourse tableTrainingCourse);
        bool Update(TableQuest tableQuestToUpdate);
        bool Delete(int id);
    }
}
