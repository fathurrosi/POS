using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using POS.Application.Interfaces;
using POS.Infrastructure.Logging;
using POS.Infrastructure.Repositories;
using System.Configuration;
using System.Net;


var builder = WebApplication.CreateBuilder(args);
string[] allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
// Add services to the container.

builder.Services.AddDbContext<POS.Infrastructure.POSContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IPrevillageRepository, PrevillageRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificMethods",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader();
        });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();


// Remove the following lines:
ILogRepository logRepository = builder.Services.BuildServiceProvider().GetRequiredService<ILogRepository>();
IHttpContextAccessor httpContextAccessor = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();
builder.Logging.AddProvider(new POSLoggerProvider(logRepository, httpContextAccessor));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kestrel
//builder.WebHost.ConfigureKestrel(options =>
//{
//    // Set the port
//    options.Listen(IPAddress.Any, 5111);
//});


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors();
//app.UseCors("AllowSpecificMethods");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
