using MediatR;
using Echo.Application.Echos.Commands.AddEcho;

namespace Echo.Application.Echos.Queries
{
    public class GetEchoQuery : IRequest<EchoViewModel?>
    {
        public int AspNetUsersId { get; set; }

    }

}
