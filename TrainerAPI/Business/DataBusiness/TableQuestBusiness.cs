using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    public class TableQuestBusiness : ITableQuestBusiness
    {
        private readonly DefaultContext _defaultContext;

        public TableQuestBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public TableQuest Create(TableQuest tableQuest)
        {
            var addResult = _defaultContext.Quests.Add(tableQuest);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1 ? addResult.Entity : null;
        }

        public TableQuest Read(int id)
        {
            var quest = _defaultContext.Quests.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return quest;
        }

        public bool Update(TableQuest tableQuestToUpdate)
        {
            var quest = _defaultContext.Quests.AsNoTracking().FirstOrDefault(x => x.Id == tableQuestToUpdate.Id);
            if (quest == null)
                return false;

            _defaultContext.Quests.Update(tableQuestToUpdate);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public bool Delete(int id)
        {
            var quest = _defaultContext.Quests.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (quest == null)
                return false;

            _defaultContext.Quests.Remove(quest);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1;
        }

        public IEnumerable<TableQuest> List()
        {
            var quests = _defaultContext.Quests.AsNoTracking();
            return quests;
        }

        public List<TableQuest> QuestsList(TableTrainingCourse tableTrainingCourse)
        {
            List<TableQuest> quests = (from q in _defaultContext.Quests
                                       where q.TrainingCourseId == tableTrainingCourse.Id
                                       select q).ToList();

            return quests;

        }
    }
}
