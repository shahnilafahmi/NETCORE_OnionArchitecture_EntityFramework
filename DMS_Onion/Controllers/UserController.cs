using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceLayer.Service.Contract;
using DomainLayer.Model;

namespace DMS_Onion.Controllers
{
    [EnableCors("Security")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet("GetAllUser")]
        public async Task<List<User>> GetAllUser()
        {
            return  _user.GetAllUsers();
        }
        [HttpPost("AddUser")]
        public async void  AddUser(User model)
        {
             _user.AddUser(model);
        }

        [HttpPut("UpdateUser")]
        public async void UpdateUser(User model)
        {
            _user.UpdateUser(model);
        }

        [HttpDelete("DeleteUser")]
        public async void DeleteUser(int userId)
        {
            _user.DeleteUser(userId);
        }
    }
}
