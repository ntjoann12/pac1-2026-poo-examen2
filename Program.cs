using Microsoft.EntityFrameworkCore;
using PersonsApp.DataBase;
using PersonsApp.Services.Persons;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PersonsDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); //Espera un generico <>
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();//Todas las clases que terminen con controller, buscara al archivo que tenga ese nombre y si lo encuentra lo va a mapear.

app.Run();
