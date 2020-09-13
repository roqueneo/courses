using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Aplication.Courses;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Course>>> Get()
        {
            CourseListRequest request = new CourseListRequest();
            var courses = await _mediator.Send(request);
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetDetail(int id)
        {
            CourseRequest request = new CourseRequest{CourseId = id};
            return await _mediator.Send(request);
        }
    }
}