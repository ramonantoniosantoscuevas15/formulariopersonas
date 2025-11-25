namespace FormularioPersonas.Entidades
{
    public class Correo
    {
        public int Id { get; set; }
        public string Corrreo { get; set; } = null!;
        public int PersonaId { get; set; }
        //public Persona Persona { get; set; } = null!;
    }
}
