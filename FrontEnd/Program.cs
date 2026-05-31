using FrontEnd.Services;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Habilitar controller / views / models / validaciones
builder.Services.AddControllersWithViews();

// Habilitar guardar sesiones en memoria
builder.Services.AddSession();

// Habilitar el acceso al HttpContext desdes servicios
builder.Services.AddHttpContextAccessor();


// El sistema de login se basa en cookies, por lo que se configura la autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    // Configurar comportamiento de las cookies
    .AddCookie(options =>
    {
        // Si el usuario no está autenticado, redirigirlo a la página de login
        options.LoginPath = "/Auth/Login";
        // Si el usuario intenta acceder a una página sin permisos, redirigirlo a la página de acceso denegado
        options.AccessDeniedPath = "/Dashboard/Index";

        // Configurar la duración de la cookie de autenticación
        // este 360 viene por parte del BackEnd, que es el tiempo de expiración del token JWT, por lo que la cookie debe expirar al mismo tiempo que el token
        // options.ExpireTimeSpan = TimeSpan.FromMinutes(360);
        //         options.SlidingExpiration = true;

    });

// Configurar autorización
// Uso de [Authorize] en controladores o acciones para protegerlos
builder.Services.AddAuthorization();


// Registrar un cliente HTTP reutilizable para comunicarse con la API del BackEnd
builder.Services.AddHttpClient("BackEndApi", client =>
{
    // Url base
    client.BaseAddress = new Uri("http://localhost:5256/");
});

// Registrar servicios personalizados
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<IPeriodoService, PeriodoService>();
builder.Services.AddScoped<IAsignaturasService, AsignaturasService>();
builder.Services.AddScoped<ITareaService, TareaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Obligar al navegador a usar HTTPS en producción
    app.UseHsts();
}

// Si entra por https://localhost:5256, redirigir a https://localhost:5256
app.UseHttpsRedirection();

// Analiza la URL y determina el controlador y la acción a ejecutar, además de servir archivos estáticos como CSS, JS, imágenes, etc.
app.UseRouting();

// Habilitar sesiones, autenticación y autorización. MIddleware que se ejecuta en orden, por lo que el orden es importante
app.UseSession();

// Lee la cookie del usuario
// Aparte de que llena al httpContext.User
app.UseAuthentication();

// Verifica si el usuario tiene permisos para acceder a la ruta solicitada
app.UseAuthorization();

// Permite servir archivos estáticos desde wwwroot, como CSS, JS, imágenes, etc. Además de permitir servir archivos estáticos desde otras rutas como /assets, /lib, etc.
app.MapStaticAssets();

// Configurar rutas para controladores y acciones. Si no se especifica, se redirige a Auth/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
