using MediatR;
using System;

namespace Echo.Core.Abstractions.Events
{
    public interface IDomainEvent: IEvent
    {
        DateTime OccurredOn => DateTime.Now;

    }
    public interface IEvent : INotification
    {
    }

}