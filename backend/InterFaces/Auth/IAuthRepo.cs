using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataBase.Models;
using backend.Dto.Auth;

namespace backend.InterFaces
{
    public interface IAuthRepo
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);

    }
}