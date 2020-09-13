using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Aplication.Courses.Query;

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
        public async Task<IList<Course>> Get()
        {
            CourseListRequest request = new CourseListRequest();
            return await _mediator.Send(request);
        }
    }
}