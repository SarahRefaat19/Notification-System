
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Domain.Events;
using NotificationSystem.Domain.NewFolder;

namespace NotificationSystem.Application
{
    public class EventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Dispatch<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var Handler in handlers)
            {
                await Handler.Handle(@event);
            }

        }
    }
}
