
using CapaNegocios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<CN_Membresias>();
builder.Services.AddTransient<CN_MembresiaContenido>();
builder.Services.AddTransient<CN_AprobacionMembresiaContenido>();
builder.Services.AddTransient<CN_Correo>();
builder.Services.AddTransient<CN_Notificaciones>();
builder.Services.AddTransient<CN_Tablero>();
builder.Services.AddTransient<CN_CuidadosDelTatuaje>();
//builder.Services.AddTransient<CN_Productos>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
