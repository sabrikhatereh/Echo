using MediatR;
using Echo.Application.Echos.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Echo.Application.Echos.Commands.AddEcho
{
    public class CreateEchoCommand : IRequest<EchoViewModel>
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}
