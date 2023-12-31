using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FourTaskConnection") ?? throw new InvalidOperationException("Connection string 'FourTaskConnection' not found.");

builder.Services.AddDbContext<FourTaskContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<FourTaskContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
