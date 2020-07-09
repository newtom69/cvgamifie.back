using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface IQuestBusiness
    {
        Quest Create(Quest user);
        Quest Read(int id);
        IEnumerable<Quest> List();
        bool Update(Quest questToUpdate);
        bool Delete(int id);
    }
}
