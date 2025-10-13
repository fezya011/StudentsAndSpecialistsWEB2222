using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListStudent
{
    public class CommandListGroupIndex : IRequest<IEnumerable<StudentDTO>>
    {
        public int IndexGroup { get; set; }
        public class ListGroupIndexCommandHandler :
            IRequestHandler<CommandListGroupIndex, IEnumerable<StudentDTO>>
        {
            private readonly Db131025Context db;
            public ListGroupIndexCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<StudentDTO>> HandleAsync(CommandListGroupIndex request, CancellationToken ct = default)
            {
                return db.Students.Where(s => s.IdGroup == request.IndexGroup).Select(s => new StudentDTO { Id = s.Id, FirstName = s.FirstName, Gender = s.Gender, IdGroup = s.IdGroup, LastName = s.LastName, Phone = s.Phone });
            }
        }
    }
}
