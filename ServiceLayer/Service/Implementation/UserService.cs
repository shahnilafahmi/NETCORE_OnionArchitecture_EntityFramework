using DomainLayer.Model;
using RepositoryLayer.DBContextLayer;
using ServiceLayer.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Implementation
{
    public class UserService : IUser
    {
        private readonly AppDBContext _dbContext;
        public UserService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddUser(User model)
        {
            _dbContext.tblUsers.AddAsync(model);
            _dbContext.SaveChanges();
           
        }

        public void DeleteUser(int userId)
        {
           var user =  _dbContext.tblUsers.Find(userId);
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.tblUsers.ToList();
        }

        public User GetUserById(int userId)
        {
            return _dbContext.tblUsers.Find(userId);
            //return _dbContext.tblUsers.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public void UpdateUser(User model)
        {
            var userUpdate = _dbContext.tblUsers.Find(model.UserId);
            if(userUpdate != null)
            {
                userUpdate.UserName = model.UserName;
                userUpdate.UserPhone = model.UserPhone;
                userUpdate.UserEmailId = model.UserEmailId;
                userUpdate.UserAddress = model.UserAddress;

                _dbContext.tblUsers.Update(userUpdate);
                _dbContext.SaveChanges();
            }
        }
    }
}
