using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Domain;
using MediatR;
using Persistence;

namespace Application.Instructors
{
    public class UpdateInstructorRequest : IRequest<Instructor>
    {
        public Guid InstructorId { get; set; }
        
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Grade { get; set; }
    }

    public class UpdateInstructorHandler : IRequestHandler<UpdateInstructorRequest, Instructor>
    {
        private readonly CoursesDbContext _context;

        public UpdateInstructorHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<Instructor> Handle(UpdateInstructorRequest request, CancellationToken cancellationToken)
        {
            Instructor instructor = await _context.Instructor.FindAsync(request.InstructorId);
            if (instructor == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, new { instructor = $"Instuctor with identifier {request.InstructorId} not found"});
                
            instructor.Name = request.Name ?? request.Name;
            instructor.LastName = request.LastName ?? request.LastName;
            instructor.Grade = request.Grade ?? request.Grade;

            int executedTransactions = await _context.SaveChangesAsync();

            if (executedTransactions == 0)
                throw new Exception("Instructor can't be updted");

            return instructor;
        }
    }
}