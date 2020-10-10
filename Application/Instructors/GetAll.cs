using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Instructors
{
    public class GetAllInstructorsRequest : IRequest<IList<Instructor>> 
    {}

    public class GetAllInstructorsHandler : IRequestHandler<GetAllInstructorsRequest, IList<Instructor>>
    {
        private readonly CoursesDbContext _context;

        public GetAllInstructorsHandler(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Instructor>> Handle(GetAllInstructorsRequest request, CancellationToken cancellationToken)
            => await _context.Instructor.ToListAsync();
    }
}