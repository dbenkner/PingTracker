using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PingTracker.Data;
using System.Threading.RateLimiting;

var MyAllowSpecificOrgins = "_myAllowSpecificOrgins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PingTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevDb") ?? throw new InvalidOperationException("Connection string 'PingTrackerContext' not found.")));

// Add services to the container.
builder.Services.AddRateLimiter(o => o.AddFixedWindowLimiter(policyName: "fixed", o =>
{
    o.PermitLimit = 4;
    o.Window = TimeSpan.FromSeconds(10);
    o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    o.QueueLimit = 2;
}));

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrgins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
        });
});
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowSpecificOrgins);
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
