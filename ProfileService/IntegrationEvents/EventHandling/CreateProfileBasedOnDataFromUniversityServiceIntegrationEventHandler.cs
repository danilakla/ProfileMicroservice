

using EventBus.Abstructions;
using UniversityApi.IntegrationEvents.Events;

public class CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler :
        IIntegrationEventHandler<CreateProfileBaseOnUniverDataIntegrationEvent>

{
    public Task Handle(CreateProfileBaseOnUniverDataIntegrationEvent @event)
    {
        Console.WriteLine("Dsd");
        Console.WriteLine("test");
        throw new NotImplementedException();
    }
}
