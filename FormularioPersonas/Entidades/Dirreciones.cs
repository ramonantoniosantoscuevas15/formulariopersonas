namespace FormularioPersonas.Entidades
{
    public class Dirreciones
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Dirrecion { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public int CodigoPostar { get; set; }
        public string Pais { get; set; } = null!;
        public int PersonaId { get; set; }
    }
}
