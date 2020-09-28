using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Courses;

namespace WebApi.Controllers
{
    public class CoursesController : ApiControllerBase
    {
        public CoursesController(IMediator mediator)
            : base(mediator)
        { }

        [HttpGet]
        public async Task<ActionResult<IList<Course>>> Get()
        {
            GetAllCoursesRequest request = new GetAllCoursesRequest();
            var courses = await _mediator.Send(request);
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetDetail(int id)
        {
            GetCourseByIdRequest request = new GetCourseByIdRequest{CourseId = id};
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Add(AddCourseRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> Update(int id, UpdateCourseRequest request)
        {
            request.CourseId = id;
            return await _mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            DeleteCourseRequest request = new DeleteCourseRequest{CourseId = id};
            return await _mediator.Send(request);
        }
    }
}