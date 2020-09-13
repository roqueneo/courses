using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Courses
{
    public class CourseListRequest : IRequest<IList<Course>> 
    {}

    public class CourseListHandler : IRequestHandler<CourseListRequest, IList<Course>>
    {
        private readonly CoursesDbContext _context;

        public CourseListHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Course>> Handle(CourseListRequest request, CancellationToken cancellationToken)
        {
            var allCourses = await _context.Course.ToListAsync();
            return allCourses;
        }
    }
}