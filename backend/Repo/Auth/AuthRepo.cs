using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataBase;
using backend.DataBase.Models;
using backend.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repo.Auth
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext _db;
        public AuthRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user!;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var AddUser = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return AddUser.Entity;
        }


    }
}