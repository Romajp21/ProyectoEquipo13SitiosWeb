using CapaNegocio;
using CapaNegocios;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios antes de construir la aplicación
builder.Services.AddRazorPages();
builder.Services.AddTransient<CN_Galeria>(); // Registro del servicio para CN_Galeria
builder.Services.AddTransient<CN_GaleriaCategorias>(); // Registro del servicio para CN_GaleriaCategorias
//builder.Services.AddTransient<CN_CategoriaTatuajes>(); // Registrar CN_CategoriaTatuajes
builder.Services.AddScoped<CN_HorarioArtista>();
builder.Services.AddTransient<CN_AgendaArtista>();
builder.Services.AddTransient<CN_ArtistaCitas>();
builder.Services.AddTransient<CN_Tablero>();
// Agregar servicios para usar sesiones
builder.Services.AddDistributedMemoryCache(); // Requerido para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Cookie solo accesible desde HTTP
    options.Cookie.IsEssential = true; // Requerido para GDPR
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Habilitar middleware de sesiones
app.UseSession();

app.UseAuthorization();
app.MapRazorPages();
app.Run();
