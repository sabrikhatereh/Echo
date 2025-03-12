using Echo.Core.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Echo.Core.Events
{
    public record EchoCreatedDomainEvent(Guid Id, Guid UserId, string Text,
        DateTime EchoDate, string Hash) : IDomainEvent;
}
