namespace FormularioPersonas.Entidades
{
    public class Telefonos
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string CodigoPais { get; set; } = null!;
        public int Numero { get; set; }

    }
}
