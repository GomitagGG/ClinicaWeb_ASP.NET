// ============================================================
// PUNTO DE ENTRADA DE LA APLICACIÓN
//
// Ejecución:
//   dotnet run
//   La app escucha en https://localhost:5001 (o el puerto en launchSettings.json).
//
// Conexión a la base de datos:
//   Motor:  MySQL (via Pomelo.EntityFrameworkCore.MySql)
//   Config: appsettings.json → "ConnectionStrings:DefaultConnection"
//   Ejemplo de cadena de conexión:
//     "Server=localhost;Database=clinicadb;User=root;Password=tu_clave;"
//
// Migraciones (crear/actualizar tablas):
//   dotnet ef migrations add <Nombre>
//   dotnet ef database update
// ============================================================
using Microsoft.EntityFrameworkCore;
using ClinicaWeb.Data;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClinicaContext>(options =>
    options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Middleware: solo restringir acceso a /Pacientes/* y /Account/Logout
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();
    if ((path.StartsWith("/pacientes") || path.StartsWith("/account/logout")) && context.Session.GetString("Admin") == null)
    {
        context.Response.Redirect("/Account/Login");
        return;
    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();