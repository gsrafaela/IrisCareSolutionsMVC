using IrisCareSolutions.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Inject Db context inside of the services of our application
builder.Services.AddDbContext<ICSolutionsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conexao")));



var app = builder.Build();
// Configurar o pipeline de solicitação HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é 30 dias. Você pode querer alterar isso para cenários de produção, consulte https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
