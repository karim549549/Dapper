using Event_Management_Task.Repositories;
using Event_Management_Task.Routes;
using Event_Management_Task.Services;
using Event_Management_Task.Services.AuthenticationService;
using Event_Management_Task.Utilities;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<BaseRepository>();
builder.Services.AddSingleton(ServiceProvider =>
{
    var configs=ServiceProvider.GetRequiredService<IConfiguration>();
    var connectionString= configs.GetConnectionString("DefaultConnection")??
                          throw new Exception("Connection string 'DefaultConnection' not found.");
    return new SqlConnectionFactory(connectionString);
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserRoute();

app.Run();
