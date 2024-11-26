namespace GestionEducativa.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Dictado> Dictados { get; set; }

        public Profesor() { }

        public Profesor(string nombre)
        {
            Nombre = nombre;
        }

        public string AsOption(int? idSeleccionado)
        {
            return $"<option value='{Id}' {(Id == idSeleccionado ? " selected" : "")}>{Nombre}</option>";
        }
    }
}
