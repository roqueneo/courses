using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Courses
{
    public class GetAllCoursesRequest : IRequest<IList<Course>> 
    {}

    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesRequest, IList<Course>>
    {
        private readonly CoursesDbContext _context;

        public GetAllCoursesHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Course>> Handle(GetAllCoursesRequest request, CancellationToken cancellationToken)
        {
            var allCourses = await _context.Course.ToListAsync();
            return allCourses;
        }
    }
}