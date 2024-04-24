using Microsoft.EntityFrameworkCore;
using Tokobagus.Middlewares;
using Tokobaju.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECT"))
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersistence, Persistence>();
builder.Services.AddTransient<HandleExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HandleExceptionMiddleware>();

app.UseHttpsRedirection();

app.Run();
