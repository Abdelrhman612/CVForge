using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataBase.Models;
using backend.Dto.Auth;
using backend.InterFaces;
using backend.Utils;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _repo;
        private readonly IJwtService _jwt;
        public AuthService(IAuthRepo repo, IJwtService jwt)
        {
            _jwt = jwt;
            _repo = repo;
        }
        public async Task<SignUpDto> SignUpAsync(SignUpDto dto)
        {
            var isEmailExists = _repo.GetUserByEmailAsync(dto.Email).Result?.Email;

            if (isEmailExists == dto.Email)
            {
                throw new Exception("User already exists");

            }

            var HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var Password = dto.Password = HashPassword;

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword
            };

            var addedUser = await _repo.AddUserAsync(newUser);


            var UserName = addedUser.Name = dto.Name;
            var Email = addedUser.Email = dto.Email;



            var result = new SignUpDto
            {
                Name = UserName,
                Email = Email,
                Password = Password
            };
            return result;
        }


        public async Task<string> SignInAsync(SignInDto dto)
        {
            var user = _repo.GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Result.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }
            var payload = new User
            {
                Id = user.Result.Id,
                Name = user.Result.Name,
                Email = user.Result.Email
            };
            var token = _jwt.GenerateToken(payload);
            return token;
        }

    }



}

