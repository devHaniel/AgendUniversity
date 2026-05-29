using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Periodo> Periodos { get; set; } = new();
    }
}
