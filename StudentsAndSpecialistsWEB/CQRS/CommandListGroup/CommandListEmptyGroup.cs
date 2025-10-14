using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using StudentsAndSpecialistsWEB.CQRS.CommandListStudent;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListGroup
{
    public class CommandListEmptyGroup : IRequest<IEnumerable<GroupDTO>>
    {
        public class ListEmptyGroupCommandHandler :
          IRequestHandler<CommandListEmptyGroup, IEnumerable<GroupDTO>>
        {
            private readonly Db131025Context db;
            public ListEmptyGroupCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<GroupDTO>> HandleAsync(CommandListEmptyGroup request, CancellationToken ct = default)
            {
                return db.Groups.Where(s=>s.Students.Count()==0).Select(s => new GroupDTO { Id = s.Id, Title = s.Title, IdSpecial = s.IdSpecial });
            }
        }
    }
}
