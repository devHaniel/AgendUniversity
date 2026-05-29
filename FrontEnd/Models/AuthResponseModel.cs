using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class AuthResponseModel
    {
        public UsuarioModel Usuario { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        // Periodos omitted: add if needed for views
    }
}
