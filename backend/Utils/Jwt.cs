using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Utils
{
    public class Jwt
    {

        public string Key { get; set; } = "";
        public string Audience { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string LifeTime { get; set; } = "";

    }
}