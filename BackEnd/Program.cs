using System.Text;
using BackEnd.EndPoints;
using BackEnd.Mapper;
using BackEnd.Persistencia;
using BackEnd.Repository;
using BackEnd.Repository.Interfaces;
using BackEnd.Service;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Conexi�n no encontrada.");

builder.Services.AddDbContext<AppDbContext>(opciones =>
{
    opciones.UseSqlServer(connectionString, sqlOptions => {
        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

builder.Services.AddOpenApi();

// Configurar Permisos CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("todos", policy =>
    {
        policy.WithOrigins("http://localhost:5256", "https://localhost:5256",
                          "http://localhost:5263", "https://localhost:7119")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar JwT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!))
        };
    });


builder.Services.AddAutoMapper(cfg =>
{
}, typeof(UsuarioProfile).Assembly);

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
builder.Services.AddScoped<IPeriodoRepository, PeriodoRepository>();

// Services
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IAsignaturaService, AsignaturaService>();
builder.Services.AddScoped<IPeriodoService, PeriodoService>();
builder.Services.AddScoped<AuthorizationService>();

// Autenticacion
builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("todos");

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch(Exception ex)
    {
        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(new
        {
            error = ex.Message
        });
    }
} );

app.MapAuthEndpoints();
app.MapUsuarioEndpoints();
app.MapPeriodoEndpoints();
app.MapAsignaturaEndpoints();
app.MapTareaEndpoints();
app.MapScalarApiReference();



app.Run();
