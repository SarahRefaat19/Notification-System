using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using NotificationSystem;
using NotificationSystem.Application;
using NotificationSystem.Application.Handlers;
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Application.Services;
using NotificationSystem.Domain.Events;
using NotificationSystem.Domain.NewFolder;
using NotificationSystem.Insfrastructure;
using NotificationSystem.Insfrastructure.SignlR;
using ErrorEventHandler = NotificationSystem.Application.Handlers.ErrorEventHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseInMemoryDatabase("NotificationDb"));

builder.Services.AddScoped<INotificationService,NotificationService>();
builder.Services.AddScoped<IInAppNotificationService,InAppNotificationService>();
builder.Services.AddScoped<IEmailNotificationService,EmailNotificationService>();
builder.Services.AddScoped<NotificationRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserNotificationRepository>();
builder.Services.AddScoped<IEventHandler<BookingApprovedEvent>, BookingApprovedHandler>();
builder.Services.AddScoped<IEventHandler<ErrorEvent>, ErrorEventHandler>();
builder.Services.AddScoped<EventDispatcher>();
builder.Services.AddScoped<EventPublisher>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(Config => Config.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();
app.MapHub<NotificationHub>("/notificationHub");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/test", async (EventPublisher publisher) =>
{
     publisher.Publish(new BookingApprovedEvent { bookingId = 2, Message = "Booking Approved" });
    return "Done";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
