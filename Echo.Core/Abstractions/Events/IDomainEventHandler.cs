using MediatR;

namespace Echo.Core.Abstractions.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
      where TEvent : IEvent
    {
    }

}