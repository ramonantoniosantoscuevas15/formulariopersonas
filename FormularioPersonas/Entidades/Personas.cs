namespace FormularioPersonas.Entidades
{
    public class Personas
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public List<Dirreciones> Dirreciones { get; set; } = new List<Dirreciones>();
       
    }
}
