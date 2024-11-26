namespace GestionEducativa.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int SemestreId { get; set; }
        public Semestre Semestre { get; set; }
        public List<Dictado> Dictados { get; set; }
        public Materia() { }
        public Materia(string nombre, string descripcion, int semestreId)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            SemestreId = semestreId;
        }

        public bool EsValido()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Descripcion) || SemestreId <= 0)
            {
                return false;
            }
            return true;
        }

        public string AsOption(int? idSeleccionado)
        {
            return $"<option value='{Id}'{(Id == idSeleccionado ? " selected" : "")}>{Semestre?.Carrera.Nombre} - {Nombre}</option>";
        }
    }
}
