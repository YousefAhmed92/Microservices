using MediatR;
using System.Reflection;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        public Guid EventId => Guid.NewGuid();

        public DateTime OccurredOn { get; }

        public string EventType => GetType().AssemblyQualifiedName;
    }
}
