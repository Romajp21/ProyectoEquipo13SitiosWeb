using CapaDatos;
using CapaNegocio;
using CapaNegocios;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración para obtener la cadena de conexión
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configurar los servicios de cada capa
builder.Services.AddSingleton<Conexion>(); // Registra la conexión con acceso a IConfiguration
builder.Services.AddTransient<ProductoDatos>(); // Servicio de datos
builder.Services.AddTransient<CN_Tablero>(); // Servicio de negocios
builder.Services.AddTransient<ProductoNegocios>(); // Servicio de negocios

// Agregar CN_Citas y CN_AgendaArtista al contenedor de servicios
builder.Services.AddScoped<CN_Citas>();
builder.Services.AddTransient<CapaNegocios.CN_AgendaArtista>();
builder.Services.AddTransient<CapaNegocios.CN_Citas>();

builder.Services.AddTransient<CN_Roles>();
builder.Services.AddSingleton<CN_CuidadosDelTatuaje>();

// Configurar soporte para sesiones
builder.Services.AddDistributedMemoryCache(); // Utiliza memoria para almacenar sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de vida de la sesión
    options.Cookie.HttpOnly = true; // Mejora la seguridad al hacer que la cookie sea accesible solo desde HTTP
    options.Cookie.IsEssential = true; // Marca la cookie como esencial (importante para GDPR)
});

// Agregar Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuración del pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar sesiones
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
