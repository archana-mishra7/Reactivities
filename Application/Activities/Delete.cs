using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
                {
                    public Guid ID { get; set; }
                }
        
                public class Handler : IRequestHandler<Command> 
                {
                    private readonly DataContext _context;
                    public Handler(DataContext context)
                    {
                        _context = context;
        
                    }
                    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
                    {
                       var activty = await _context.Activities.FindAsync(request.ID);
                       if(activty==null)
                        throw new Exception("Could not find Activity");

                        _context.Remove(activty);
                        
                        var sucess = await _context.SaveChangesAsync() >0;
                        if(sucess) return Unit.Value;
                        throw new Exception("Problem Saving changes");
                    }
                }
    }
}