using Core.Services;
using Core.Services.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CONTEXT

builder.Services.AddDbContext<DevLogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevlogConnection"));
});

#endregion

#region IoC

builder.Services.AddScoped<IUserService, UserService>();

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", build =>
    {
        build
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EnableCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
