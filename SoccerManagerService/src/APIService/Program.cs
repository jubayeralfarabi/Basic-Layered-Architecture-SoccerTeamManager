using Microsoft.EntityFrameworkCore;
using Soccer.Infrastructure.Repository.RDBRepository.DbContexts;
using Soccer.Platform.Infrastructure.Extensions;
using Soccer.Application.CommandHandlers;
using Soccer.Application.Commands;
using Soccer.Platform.Infrastructure.Core.Commands;
using Services.Abstractions;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();


IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

builder.Services.AddDbContext<SoccerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AlterationDbContext")));
builder.Services.AddCore();

builder.Services.AddScoped<ICommandHandlerAsync<CreateUserCommand>, CreateUserCommandHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
//builder.Services.AddScoped<ITransferService, TransferService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
