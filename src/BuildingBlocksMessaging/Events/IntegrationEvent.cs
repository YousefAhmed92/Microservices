namespace BuildingBlocksMessaging.Events
{
    public record IntegrationEvent
    {
        public Guid Id => Guid.NewGuid();

        public DateTime OccuredAt => DateTime.Now;

        public string EventType => GetType().AssemblyQualifiedName; 
    }
}
