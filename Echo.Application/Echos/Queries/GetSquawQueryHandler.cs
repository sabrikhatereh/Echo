using MediatR;
using Echo.Application.Abstractions.DbContexts;
using System;
using System.Threading.Tasks;
using System.Threading;
using Echo.Application.Echos.Commands.AddEcho;

namespace Echo.Application.Echos.Queries
{
    public class GetSquawQueryHandler : IRequestHandler<GetEchoQuery, EchoViewModel?>
    {
        private readonly IApplicationReadDb db;

        public GetSquawQueryHandler(IApplicationReadDb db) => this.db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<EchoViewModel?> Handle(GetEchoQuery request, CancellationToken cancellationToken)
        {
          
            return null;
        }
    }

}
