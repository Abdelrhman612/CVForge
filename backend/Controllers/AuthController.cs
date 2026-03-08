using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dto.Auth;
using backend.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(SignUpDto dto)
        {
            var result = await _service.SignUpAsync(dto);
            return Ok(result);
        }
        [HttpPost("signin")]
        public async Task<ActionResult> SignIn(SignInDto dto)
        {
            var result = await _service.SignInAsync(dto);
            return Ok(result);
        }

    }
}