using Autofac.Extensions.DependencyInjection;
using EventBus.Abstructions;
using ProfileService.Extensions;
using UniversityApi.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIntegrationServices(builder.Configuration);
builder.Services.AddEventBus(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
ConfigureEventBus(app);

app.Run();
void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<CreateProfileBaseOnUniverDataIntegrationEvent, CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler>();
}