using System;
using System.Collections.Generic;
using FrontEnd.Models.Periodo;

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
        public List<FrontEnd.Models.Periodo.Periodo> Periodos { get; set; } = new();
    }
}
