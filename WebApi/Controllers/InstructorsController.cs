using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Instructors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class InstructorsController : ApiControllerBase
    {
        public InstructorsController(IMediator mediator) 
            : base(mediator)
        { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetAll()
        {
            var request = new GetAllInstructorsRequest();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Instructor>> Add(AddInstructorRequest request)
            => await _mediator.Send(request);
    }
}