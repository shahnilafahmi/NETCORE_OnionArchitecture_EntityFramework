using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Contract
{
    public interface IUser
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);

        void AddUser(User model);

        void UpdateUser(User model);

        void DeleteUser(int userId);
    }
    
}
