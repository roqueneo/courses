using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Enumerations;
using Application.Error;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Courses
{
    public class UpdateInstructorRequest : IRequest
    {
        public Guid CourseId { get; set; }

        public Guid InstructorId { get; set; }

        public RequestedAction Action { get; set; }
    }

    public class UpdateInstructorRequestValidator : AbstractValidator<UpdateInstructorRequest>
    {
        public UpdateInstructorRequestValidator()
        {
            RuleFor(r => r.Action).NotEmpty();
        }
    }

    public class UpdateInstructorHandler : IRequestHandler<UpdateInstructorRequest>
    {
        private readonly CoursesDbContext _context;

        public UpdateInstructorHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInstructorRequest request, CancellationToken cancellationToken)
        {
            Course course = await _context.Course.FindAsync(request.CourseId);
            if (course == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, new { course = $"Course with identifier [{request.CourseId}] not found"});

            Instructor instructor = await _context.Instructor.FindAsync(request.InstructorId);
            if (instructor == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, new { course = $"Course with identifier [{request.CourseId}] not found"});

            course.AddInstructor(instructor);

            int executedTransactions = await _context.SaveChangesAsync();
            if (executedTransactions == 0)
                throw new Exception("Course can't be updated");

            return Unit.Value; 

        }
    }
}