using api.Data;
using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();

var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection manquante dans configuration");


builder.Services.AddDbContext<PortyDbContext>(options =>
{
    options.UseSqlServer(cs);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

//Gestion des langues
string[] supported = new[] { "fr", "en" };
builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    o.DefaultRequestCulture = new RequestCulture("fr");
    o.SupportedCultures = supported.Select(c => new CultureInfo(c)).ToList();
    o.SupportedUICultures = o.SupportedCultures;
    o.RequestCultureProviders = new IRequestCultureProvider[] {
        new AcceptLanguageHeaderRequestCultureProvider()
    };
    o.FallBackToParentCultures = true;
    o.FallBackToParentUICultures = true;
});

//Services
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IBuisness, BuisnessService>();
builder.Services.AddScoped<ICareer, CareerService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IMail, MailService>();
builder.Services.AddScoped<IProject, ProjectService>();
builder.Services.AddScoped<IUser, UserService>();

builder.Services.AddSingleton<IJsonLocalizer, JsonLocalizer>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("Front", p =>
        p.WithOrigins(
             "https://localhost",
             "https://porty.localhost",
             "http://localhost",
             "http://porty.localhost",
             "http://localhost:5173",
             "http://127.0.0.1:5173",
             "http://localhost:8080",
             "http://127.0.0.1:8080",
             "http://murierromain.com",
             "http://romainmurier.com",
             "http://test.murierromain.com",
             "http://test.romainmurier.com",
             "https://murierromain.com",
             "https://romainmurier.com",
             "https://test.murierromain.com",
             "https://test.romainmurier.com"
             )
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
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.StatusCode = StatusCodes.Status204NoContent;
        return;
    }

    await next();
});
var loc = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization();

app.MapControllers();

app.Run();
