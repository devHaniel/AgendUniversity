namespace BackEnd.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<PeriodoDto> Periodos { get; set; }
    }
}
