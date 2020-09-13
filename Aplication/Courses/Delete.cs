using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Aplication.Courses
{
    public class DeleteCourseRequest : IRequest
    {
        public int CourseId { get; set; } 
    }

    public class DeleteCourseHandler : IRequestHandler<DeleteCourseRequest>
    {
        private readonly CoursesDbContext _context;

        public DeleteCourseHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            Course course = await _context.Course.FindAsync(request.CourseId);
            if (course == null)
                throw new ArgumentNullException("CourseId", $"Course with identifier [{request.CourseId}] not found");

            _context.Remove(course);

            int executedTransactions = await _context.SaveChangesAsync();

            if (executedTransactions == 0)
                throw new Exception("Course can't be deleted");

            return Unit.Value; 
        }
    }
}