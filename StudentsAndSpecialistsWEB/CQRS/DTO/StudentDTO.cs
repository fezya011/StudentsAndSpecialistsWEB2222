namespace StudentsAndSpecialistsWEB.CQRS.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public sbyte Gender { get; set; }

        public int? IdGroup { get; set; }
    }
}
