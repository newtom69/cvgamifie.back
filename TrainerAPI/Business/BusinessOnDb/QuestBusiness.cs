using Data;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainerAPI.Business
{
    /// <summary>
    /// Classe de gestion des utilisateurs (User)
    /// </summary>
    public class QuestBusiness : IQuestBusiness
    {
        private readonly DefaultContext _defaultContext;

        public QuestBusiness(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }

        public Quest Create(Quest quest)
        {
            var addResult = _defaultContext.Quests.Add(quest);
            var saveResult = _defaultContext.SaveChanges();
            return saveResult == 1 ? addResult.Entity : null;
        }

        public Quest Read(int id)
        {
            var user = _defaultContext.Quests.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return user;
        }

        public bool Update(Quest questToUpdate)
        {
            var user = _defaultContext.Quests.AsNoTracking().FirstOrDefault(x => x.Id == questToUpdate.Id);
            if (user == null)
                return false;

            _defaultContext.Quests.Update(questToUpdate);
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

        public IEnumerable<Quest> List()
        {
            var quests = _defaultContext.Quests.AsNoTracking();
            return quests;
        }

        internal List<Quest> QuestsList(TrainingCourse trainingCourse)
        {
            List<Quest> quests = (from q in _defaultContext.Quests
                                  where q.TrainingCourseId == trainingCourse.Id
                                  select q).ToList();

            return quests;

        }
    }
}
