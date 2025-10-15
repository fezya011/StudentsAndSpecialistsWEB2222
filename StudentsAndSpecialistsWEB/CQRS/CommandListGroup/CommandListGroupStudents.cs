using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListGroup
{
    public class CommandListGroupStudents : IRequest<IEnumerable<GroupDTO>>
    {
        public class ListGroupStudentsCommandHandler :
        IRequestHandler<CommandListGroupStudents, IEnumerable<GroupDTO>>
        {
            private readonly Db131025Context db;
            public ListGroupStudentsCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<GroupDTO>> HandleAsync(CommandListGroupStudents request, CancellationToken ct = default)
            {
                return db.Groups.Include(s => s.Students).Select(s => new GroupDTO { Id = s.Id, Title = s.Title, IdSpecial = s.IdSpecial, StudentsCount = s.Students.Count });
            }
        }
    }
}
