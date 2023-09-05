using Microsoft.EntityFrameworkCore;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<PlaninaContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString(name: "Planina")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opcije =>
    {
        opcije.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(opcije =>
    {
        opcije.ConfigObject.AdditionalItems.Add("reguestSnippersEnabled", true);
    });
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
