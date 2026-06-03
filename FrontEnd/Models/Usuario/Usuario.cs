using System.Collections.Generic;
using FrontEnd.Models.Periodo;

namespace FrontEnd.Models.Usuario
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Periodo.Periodo> Periodos { get; set; } = new();
    }
}
