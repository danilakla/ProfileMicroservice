using EventBus.Events;

namespace UniversityApi.IntegrationEvents.Events;

public record  CreateProfileBaseOnUniverDataIntegrationEvent : IntegrationEvent
{
    public int ProfileId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string University { get; set; }
    public string Email { get; set; }
    public string BackPhoto { get; set; }
    public string Photo { get; set; }
    public string Role { get; set; }

}
