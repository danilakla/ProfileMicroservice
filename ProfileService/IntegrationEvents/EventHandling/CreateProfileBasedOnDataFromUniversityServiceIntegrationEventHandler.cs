

using EventBus.Abstructions;
using ProfileService.Data;
using ProfileService.Models;
using System.Text;
using UniversityApi.IntegrationEvents.Events;

public class CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler :
        IIntegrationEventHandler<CreateProfileBaseOnUniverDataIntegrationEvent>

{
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task Handle(CreateProfileBaseOnUniverDataIntegrationEvent @event)
    {
        Profiles profile = new() {
            Email = @event.Email,
            LastName = @event.LastName,
            Name = @event.Name,
            UserId = @event.ProfileId,
            BackgroundProfile = Encoding.UTF8.GetBytes(@event.BackPhoto),
            Photo = Encoding.UTF8.GetBytes(@event.Photo),
            UniversityName = @event.University,
            
        };
        await _applicationDbContext.AddAsync(profile);
        await _applicationDbContext.SaveChangesAsync();
    }
}
