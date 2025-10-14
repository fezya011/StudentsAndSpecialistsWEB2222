using MyMediator.Interfaces;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListStudent
{
    public class CommandListGenderCount : IRequest<IEnumerable<StudentDTO>>
    {
        public int IndexGroup { get; set; }
        public class ListGenderCountCommandHandler :
            IRequestHandler<CommandListGenderCount, IEnumerable<StudentDTO>>
        {
            private readonly Db131025Context db;
            public ListGenderCountCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<StudentDTO>> HandleAsync(CommandListGenderCount request, CancellationToken ct = default)
            {
                return db.Students.Where(s => s.IdGroup == request.IndexGroup).Select(s => new StudentDTO { Id = s.Id, FirstName = s.FirstName, Gender = s.Gender, IdGroup = s.IdGroup, LastName = s.LastName, Phone = s.Phone });
            }
        }
    }
}
