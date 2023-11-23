using IrisCareSolutions.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Adicionar injeção de dependência para IWebHostEnvironment
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

// Recuperar a string de conexão do appsettings.json
var conexao = builder.Configuration.GetConnectionString("conexao");

// Configurar a injeção de dependência do Contexto, utilizando também a string de conexão
builder.Services.AddDbContext<ICSolutionsContext>(options => options.UseSqlServer(conexao));

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
