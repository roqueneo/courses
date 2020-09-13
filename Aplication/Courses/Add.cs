using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Aplication.Courses
{
    public class AddCourseRequest : IRequest<Course>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; } 
    }

    public class AddCourseHandler : IRequestHandler<AddCourseRequest, Course>
    {
        private readonly CoursesDbContext _context;

        public AddCourseHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(AddCourseRequest request, CancellationToken cancellationToken)
        {
            Course course = new Course
            {
                Name = request.Name,
                Description = request.Description,
                PublicationDate = request.PublicationDate
            };
            _context.Course.Add(course);
            int executedTransactions = await _context.SaveChangesAsync();

            if (executedTransactions == 0)
                throw new Exception("Course can't be added");

            return course; 
        }
    }
}