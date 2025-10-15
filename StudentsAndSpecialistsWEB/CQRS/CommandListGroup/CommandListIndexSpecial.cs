using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListGroup
{
    public class CommandListIndexSpecial : IRequest<IEnumerable<GroupDTO>>
    {
        public class ListIndexSpecialCommandHandler :
        IRequestHandler<CommandListIndexSpecial, IEnumerable<GroupDTO>>
        {
            private readonly Db131025Context db;
            public ListIndexSpecialCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<GroupDTO>> HandleAsync(CommandListIndexSpecial request, CancellationToken ct = default)
            {
                return db.Groups.Include(s => s.Students).Select(s => new GroupDTO { Id = s.Id, Title = s.Title, IdSpecial = s.IdSpecial, StudentsCount = s.Students.Count });
            }
        }
    }
}
