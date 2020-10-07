using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Domain;
using MediatR;
using Persistence;

namespace Application.Courses
{
    public class GetCourseByIdRequest : IRequest<Course>
    {
        public Guid CourseId { get; set; }
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
            if (course == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, new { course = $"Course with identifier [{request.CourseId}] not found"});

            return course;
        }
    }
}