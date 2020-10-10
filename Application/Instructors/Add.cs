using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Instructors
{
    public class AddInstructorRequest : IRequest<Instructor>
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public string Grade { get; set; }
    }

    public class AddInstructorHandler : IRequestHandler<AddInstructorRequest, Instructor>
    {
        private readonly CoursesDbContext _context;

        public AddInstructorHandler(CoursesDbContext coursesDbContext)
        {
            _context = coursesDbContext;
        }

        public async Task<Instructor> Handle(AddInstructorRequest request, CancellationToken cancellationToken)
        {
            Instructor instructor = new Instructor(request.Name, request.LastName, request.Grade);
            await _context.AddAsync(instructor);
            int executedTransactions = await _context.SaveChangesAsync();

            if (executedTransactions == 0)
                throw new Exception("Instructor can't be added");
            
            return instructor;
        }
    }
}