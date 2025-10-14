using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMediator.Types;
using StudentsAndSpecialistsWEB.CQRS.CommandListGroup;
using StudentsAndSpecialistsWEB.CQRS.CommandListStudent;
using StudentsAndSpecialistsWEB.CQRS.DTO;

namespace StudentsAndSpecialistsWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Mediator mediator;
        public StudentsController(MyMediator.Types.Mediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("ListStudentsIndexGroup")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> ListStudentsIndexGroup(int indexGroup)
        {
            var command = new CommandListGroupIndex { IndexGroup = indexGroup };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpGet("CountGenderByIndexGroup")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> CountGenderByIndexGroup(int indexGroup)
        {
            var command = new CommandListGenderCount { IndexGroup = indexGroup };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpGet("ListUnboundStudents")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> ListUnboundStudents()
        {
            var command = new CommandListUnboundStudents();
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }
 

        [HttpGet("ListEmptyGroups")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> ListEmptyGroups()
        {
            var command = new CommandListEmptyGroup();
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

    }
}
