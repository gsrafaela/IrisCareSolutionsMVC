using IrisCareSolutions.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllersWithViews();

// Adicionar inje��o de depend�ncia para IWebHostEnvironment
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

// Recuperar a string de conex�o do appsettings.json
var conexao = builder.Configuration.GetConnectionString("conexao");

// Configurar a inje��o de depend�ncia do Contexto, utilizando tamb�m a string de conex�o
builder.Services.AddDbContext<ICSolutionsContext>(options => options.UseSqlServer(conexao));

var app = builder.Build();

// Configurar o pipeline de solicita��o HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o do HSTS � 30 dias. Voc� pode querer alterar isso para cen�rios de produ��o, consulte https://aka.ms/aspnetcore-hsts.
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
