using Microsoft.EntityFrameworkCore;
using SiteAPI.Applications.Interfaces;
using SiteAPI.Applications.Services;
using SiteAPI.Infrastructer.Database;
using SiteAPI.Infrastructer.Repsitory;

var builder = WebApplication.CreateBuilder(args);

// Add CORS service to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", policy =>
    {
        policy.AllowAnyOrigin()   // Allows requests from any origin
              .AllowAnyMethod()   // Allows any HTTP method (GET, POST, etc.)
              .AllowAnyHeader();  // Allows any header
    });
});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db injection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStr"));
});

//Dipendency injection
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ITransaction, Transaction>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS middleware
app.UseCors("AllowAny");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
