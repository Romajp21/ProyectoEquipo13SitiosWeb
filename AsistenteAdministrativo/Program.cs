using CapaDatos;
using CapaNegocio;
using CapaNegocios;

var builder = WebApplication.CreateBuilder(args);

// Agrega los servicios al contenedor.
builder.Services.AddRazorPages();
builder.Services.AddTransient<CN_Roles>();
builder.Services.AddSingleton<CapaDatos.Conexion>();
builder.Services.AddTransient<CN_Membresias>();
builder.Services.AddTransient<CN_MembresiaContenido>();
builder.Services.AddTransient<CN_Notificaciones>();
builder.Services.AddTransient<CN_Correo>();
builder.Services.AddTransient<CN_Tablero>();
var app = builder.Build();

// Configura el pipeline de solicitud HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
