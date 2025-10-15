using MyMediator.Interfaces;
using MyMediator.Types;
using StudentsAndSpecialistsWEB.DB;

namespace StudentsAndSpecialistsWEB.CQRS.CommandListGroup
{
    public class CommandAddGroupBySpecial : IRequest
    {
        public int IdSpecial { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }


        public class AddGroupBySpecialCommandHandler : IRequestHandler<CommandAddGroupBySpecial, Unit>
        {
            private readonly Db131025Context db;

            public AddGroupBySpecialCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<Unit> HandleAsync(CommandAddGroupBySpecial request, CancellationToken ct = default)
            {
                var existingTitle = db.Groups.FirstOrDefault(s=> s.Title == request.Title);
                if (existingTitle != null)
                    throw new Exception($"Такая группа: '{request.Title}' уже существует !");

                var newGroup = new Group
                {
                    Title = request.Title,
                    IdSpecial = request.IdSpecial
                };

                await db.Groups.AddAsync(newGroup);
                await db.SaveChangesAsync(ct);

                return Unit.Value;
            }
        }

    }
}
