namespace FormularioPersonas.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
    }
}
