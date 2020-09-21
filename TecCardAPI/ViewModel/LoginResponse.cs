using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecCardAPI.ViewModel
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string  Nome { get; set; }
        public string  Tipo { get; set; }

    }
}
