using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dto.Auth;

namespace backend.InterFaces
{
    public interface IAuthService
    {
        Task<SignUpDto> SignUpAsync(SignUpDto dto);
        Task<string> SignInAsync(SignInDto dto);

    }
}