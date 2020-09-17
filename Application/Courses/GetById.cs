using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Courses
{
    public class GetCourseByIdRequest : IRequest<Course>
    {
        public int CourseId { get; set; }
    }

    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdRequest, Course>
    {
        private readonly CoursesDbContext _context;

        public GetCourseByIdHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Course.FindAsync(request.CourseId);
            return course;
        }
    }
}