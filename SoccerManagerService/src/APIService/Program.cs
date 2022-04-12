using Microsoft.EntityFrameworkCore;
using Soccer.Infrastructure.Repository.RDBRepository.DbContexts;
using Soccer.Platform.Infrastructure.Extensions;
using Soccer.Application.CommandHandlers;
using Soccer.Application.Commands;
using Soccer.Platform.Infrastructure.Core.Commands;
using Services.Abstractions;
using Services;
using Soccer.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{ 
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, new string[] { }},
    });
});
builder.Services.AddApplicationInsightsTelemetry();


IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

builder.Services.AddDbContext<SoccerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SoccerDbContext")));
builder.Services.AddCore();

builder.Services.AddScoped<ICommandHandlerAsync<CreateUserCommand>, CreateUserCommandHandler>();
builder.Services.AddScoped<ICommandHandlerAsync<LoginUserCommand>, LoginUserCommandHandler>();
builder.Services.AddScoped<ICommandHandlerAsync<ConfirmTransferCommand>, ConfirmTransferCommandHandler>();
builder.Services.AddScoped<ICommandHandlerAsync<CreateTransferCommand>, CreateTransferCommandHandler>();
builder.Services.AddScoped<ICommandHandlerAsync<UpdatePlayerCommand>, UpdatePlayerCommandHandler>();
builder.Services.AddScoped<ICommandHandlerAsync<UpdateTeamCommand>, UpdateTeamCommandHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITransferService, TransferService>();

builder.Services.AddJwtBearerAuthentication();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
