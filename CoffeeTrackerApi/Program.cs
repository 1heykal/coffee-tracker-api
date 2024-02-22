using CoffeeTrackerApi.Models;
using CoffeeTrackerApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<ApplicationDbContext>
(optionBuilder => optionBuilder.UseSqlServer(sqlServerConnectionString));

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IRepository<Record>, RecordRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//builder.Services.AddHttpsRedirection(options =>  options.HttpsPort = 7089);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
