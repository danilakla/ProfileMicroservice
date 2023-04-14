using Autofac.Extensions.DependencyInjection;
using EventBus.Abstructions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfileService.Data;
using ProfileService.DTO;
using ProfileService.Extensions;
using ProfileService.Infrastructure;
using ProfileService.Infrastructure.Repository;
using ProfileService.Models;
using ProfileService.Services;
using System.Text;
using UniversityApi.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration["AppSettings:MSS"]);
});

builder.Services.AddScoped<IProfileService, ProfilesService>();
builder.Services.AddScoped<IRepository<CreateProjectDto, Projects>,  ProjectRepository>();
builder.Services.AddScoped<IRepository<CreateSkillDto, Skills>, SkillRepository>();

builder.Services.AddSingleton<IdentityService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuerSigningKey = false,
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]))
    };
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ConfigureEventBus(app);

app.Run();
void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<CreateProfileBaseOnUniverDataIntegrationEvent, CreateProfileBasedOnDataFromUniversityServiceIntegrationEventHandler>();
}