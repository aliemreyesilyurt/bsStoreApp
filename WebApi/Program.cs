using Microsoft.EntityFrameworkCore;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IoC DbContext tanimi yapildi
// IOC semasindaki Register blogu
builder.Services.AddDbContext<RepositoryContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

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
