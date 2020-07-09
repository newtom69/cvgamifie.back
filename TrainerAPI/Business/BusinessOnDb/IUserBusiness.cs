using Data.Model;
using Data.Model;
using System.Collections.Generic;

namespace TrainerAPI.Business
{
    public interface IUserBusiness
    {
        User Create(User user);
        User Read(int id);
        IEnumerable<User> List();
        bool Update(User userToUpdate);
        bool Delete(int id);
    }
}
