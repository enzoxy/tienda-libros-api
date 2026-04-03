namespace TiendaLibros.API.Autor.Modelo
{
    public class AcademicDegree {

        public int AcademicDegreeID { get; set; }
        public required string DegreeName { get; set; }
        public required string SchoolName { get; set; }
        public DateTime? GraduationDate { get; set; }

        // Entity-Framework-Core (1 grado académico se asocia a 1 autor -no puede no tener autor-)
        //public int AuthorID { get; set; } -podemos o no escribir explicitamente este campo de clave foránea.. alcanza con el objeto Author
        public required Author Author { get; set; }

        // Identificador global, para no crear conflictos entre db's de cada microservicio.
        public required string AcademicDegreeGUID { get; set; }

    }
}
