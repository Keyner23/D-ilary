using D_Ilary.Web.Data;
using D_Ilary.Web.Interfaces.IRepositories;
using D_Ilary.Web.Interfaces.IServices;
using D_Ilary.Web.Repository;
using D_Ilary.Web.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Configuraracion EF con supabase
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();


// Agrega Identity con EntityFramework
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Control de cookies y rutas por defecto
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/Index";
});


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//creamos roles por defecto para identity
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Admin", "Comprador" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // var adminEmail = "admin@example.com";
    // var adminPassword = "Admin123!"; 
    //
    // var adminUser = await userManager.FindByEmailAsync(adminEmail);
    // if (adminUser == null)
    // {
    //     var newAdmin = new IdentityUser
    //     {
    //         UserName = adminEmail,
    //         Email = adminEmail,
    //         EmailConfirmed = true
    //     };
    //
    //     var result = await userManager.CreateAsync(newAdmin, adminPassword);
    //     if (result.Succeeded)
    //     {
    //         await userManager.AddToRoleAsync(newAdmin, "Admin");
    //         Console.WriteLine("✅ Usuario admin creado correctamente");
    //     }
    //     else
    //     {
    //         Console.WriteLine("❌ Error creando admin:");
    //         foreach (var error in result.Errors)
    //             Console.WriteLine($"   - {error.Description}");
    //     }
    // }
    // else
    // {
    //     Console.WriteLine("ℹ️ Usuario admin ya existe");
    // }
}

app.Run();