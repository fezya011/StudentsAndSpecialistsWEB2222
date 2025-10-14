using MyMediator.Interfaces;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListStudent
{
    public class CommandListUnboundStudents : IRequest<IEnumerable<StudentDTO>>
    {
        public class ListUnboundStudentsCommandHandler :
            IRequestHandler<CommandListUnboundStudents, IEnumerable<StudentDTO>>
        {
            private readonly Db131025Context db;
            public ListUnboundStudentsCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<StudentDTO>> HandleAsync(CommandListUnboundStudents request, CancellationToken ct = default)
            {
                return db.Students.Where(s => s.IdGroup == null).Select(s => new StudentDTO { Id = s.Id, FirstName = s.FirstName, Gender = s.Gender, IdGroup = s.IdGroup, LastName = s.LastName, Phone = s.Phone });
            }
        }
    }
}
