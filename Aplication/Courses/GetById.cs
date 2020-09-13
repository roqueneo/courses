using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Aplication.Courses
{
    public class CourseRequest : IRequest<Course>
    {
        public int CourseId { get; set; }
    }

    public class CourseHandler : IRequestHandler<CourseRequest, Course>
    {
        private readonly CoursesDbContext _context;

        public CourseHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(CourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Course.FindAsync(request.CourseId);
            return course;
        }
    }
}