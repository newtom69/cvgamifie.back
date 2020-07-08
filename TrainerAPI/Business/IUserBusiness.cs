using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
