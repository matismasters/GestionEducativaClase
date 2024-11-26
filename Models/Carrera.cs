namespace GestionEducativa.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Requerimientos { get; set; }
        public List<Semestre> Semestres { get; set; }

        public Carrera() { }

        public Carrera(string nombre, string descripcion, string requerimientos)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Requerimientos = requerimientos;
        }

        public bool EsValido()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Descripcion) || string.IsNullOrEmpty(Requerimientos))
            {
                return false;
            }

            return true;
        }

        public string AsOption(int selectedId)
        {
            return $"<option value='{Id}'{(Id == selectedId ? " selected" : "")}>{Nombre}</option>";
        }

    }
}
