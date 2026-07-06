using NotificationSystem.Domain.Events;

namespace NotificationSystem.Application.Interfaces
{
    public interface IEventHandler<TEvent> where TEvent:IEvent
    {
        Task Handle(TEvent @event);

    }
}
