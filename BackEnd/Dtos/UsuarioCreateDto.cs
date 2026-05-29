using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class UsuarioCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
