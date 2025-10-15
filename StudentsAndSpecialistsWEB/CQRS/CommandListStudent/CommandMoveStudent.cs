using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using MyMediator.Types;
using StudentsAndSpecialistsWEB.CQRS.CommandListGroup;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListStudent
{
    public class CommandMoveStudent : IRequest
    {
        public int IdStudent { get; set; }
        public int IdNewGroup { get; set; }

        public class MoveStudentCommandHandler : IRequestHandler<CommandMoveStudent, bool>
        {
            private readonly Db131025Context db;

            public MoveStudentCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<bool> HandleAsync(CommandMoveStudent request, CancellationToken ct = default)
            {
                var student = db.Students.FirstOrDefault(s => s.Id == request.IdStudent);
                if (student == null)
                    return false;

                var group = await db.Groups.AnyAsync(g => g.Id == request.IdNewGroup);
                if (!group)
                    return false;

                student.IdGroup = request.IdNewGroup;
                await db.SaveChangesAsync(ct);
            }
        }
    }
}
