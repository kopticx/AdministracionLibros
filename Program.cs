using AdministracionLibros.Helpers;
using AdministracionLibros.Models;
using AdministracionLibros.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();
builder.Services.AddTransient<IRoleStore<Rol>, RoleStore>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}
);

builder.Services.AddIdentity<Usuario, Rol>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
}).AddDefaultTokenProviders()
.AddErrorDescriber<MensajesDeErrorIdentity>();

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/libros/index";
    });

builder.Services.AddTransient<SignInManager<Usuario>>();
builder.Services.AddTransient<ISqlServerProvider, SqlServerProvider>();
builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddTransient<IRepositorioRol, RepositorioRoles>();
builder.Services.AddTransient<IRepositorioAutores, RepositorioAutores>();
builder.Services.AddTransient<IRepositorioEditoriales, RepositorioEditoriales>();
builder.Services.AddTransient<IRepositorioLibros, RepositorioLibros>();
builder.Services.AddTransient<IRepositorioReview, RepositorioReview>();
builder.Services.AddTransient<IServicioCloudinary, ServicioCloudinary>();
builder.Services.AddTransient<MySqlConHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=Index}/{id?}");

app.Run();
