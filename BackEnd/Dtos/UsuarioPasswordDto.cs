using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class UsuarioPasswordDto
    {
        public string Email {get; set;}
        public string OldPassword {get; set;}
        public string NewPassword {get; set;}
    }
}