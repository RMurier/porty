using api.Data;
using api.Interfaces;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection manquante dans appsettings.json");

builder.Services.AddDbContext<PortyDbContext>(options =>
{
    options.UseSqlServer(cs);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

//Services
builder.Services.AddScoped<IMail, MailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("Front", p =>
        p.WithOrigins(
             "http://localhost:5173",
             "http://127.0.0.1:5173",
             "http://localhost:8080",
             "http://127.0.0.1:8080")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Front");

app.MapControllers();

app.Run();
