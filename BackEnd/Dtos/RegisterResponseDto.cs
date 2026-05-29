using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class RegisterResponseDto
    {
        public UsuarioDto Usuario { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
