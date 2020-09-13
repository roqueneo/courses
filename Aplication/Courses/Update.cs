using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Aplication.Courses
{
    public class UpdateCourseRequest : IRequest<Course>
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? PublicationDate { get; set; } 
    }

    public class UpdateCourseHandler : IRequestHandler<UpdateCourseRequest, Course>
    {
        private readonly CoursesDbContext _context;

        public UpdateCourseHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            Course course = await _context.Course.FindAsync(request.CourseId);
            if (course == null)
                throw new ArgumentNullException("CourseId", $"Course with identifier [{request.CourseId}] not found");

            course.Name = request.Name ?? course.Name;
            course.Description = request.Description ?? course.Description;
            course.PublicationDate = request.PublicationDate ?? course.PublicationDate;

            int executedTransactions = await _context.SaveChangesAsync();

            if (executedTransactions == 0)
                throw new Exception("Course can't be added");

            return course; 
        }
    }
}