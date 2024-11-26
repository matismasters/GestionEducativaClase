namespace GestionEducativa.Models
{
    public class Dictado
    {
        public int Id { get; set; }
        public int Ano { get; set; }
        public string Turno {  get; set; }
        public string Instituto { get; set; }
        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        public Dictado() { }

        public Dictado(int ano, string turno, string instituto, int profesorId, int materiaId)
        {
            Ano = ano;
            Turno = turno;
            Instituto = instituto;
            MateriaId = materiaId;
            ProfesorId = profesorId;
        }

        public bool EsValido()
        {
            if (Ano < DateTime.Today.Year) return false;
            if (string.IsNullOrEmpty(Turno)) return false;
            if (string.IsNullOrEmpty(Instituto)) return false;
            if (ProfesorId == null|| ProfesorId == 0) return false;
            if (MateriaId == null || MateriaId == 0) return false;
            return true;
        }
    }
}
