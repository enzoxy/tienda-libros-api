namespace TiendaLibros.API.Autor.Modelo
{
    public class Author {

        public int AuthorID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        // Entity-Framework-Core (1 autor puede tener uno o más (N) grados académicos -o ninguno-).
        public ICollection<AcademicDegree> AcademicDegrees { get; set; } = [];

        // Identificador global, para no crear conflictos entre db's de cada microservicio.
        public required string AuthorGUID { get; set; }

    }
}
