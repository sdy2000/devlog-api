using Core.Services;
using Core.Services.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region CONTEXT

builder.Services.AddDbContext<DevLogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevlogConnection"));
});

#endregion

#region IoC

builder.Services.AddScoped<IUserService, UserService>();

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
