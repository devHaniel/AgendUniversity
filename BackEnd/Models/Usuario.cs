using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<Periodo> Periodos { get; set; } = new();
    }
}
