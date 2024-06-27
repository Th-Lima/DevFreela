using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery(query));

            return Ok(projects);
        }

        // api/projects/2
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/projects/2
        [HttpPut]
        [Authorize(Roles = "client")]
        public IActionResult Put([FromBody] UpdateProjectCommand command)
        {
            _mediator.Send(command);

            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));

            return NoContent();
        }

        // api/projects/comments POST
        [HttpPost("comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment([FromBody] CreateProjectCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            await _mediator.Send(new StartProjectCommand(id));

            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish([FromBody] FinishProjectCommand command)
        {
            await _mediator.Send(new FinishProjectCommand()
            {
                CreditCardNumber = command.CreditCardNumber,
                Cvv = command.Cvv,
                ExpiresAt = command.ExpiresAt,
                FullName = command.FullName,
                Id = command.Id
            });

            return NoContent();
        }
    }
}
