using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataBase.Models;

namespace backend.InterFaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);

    }
}