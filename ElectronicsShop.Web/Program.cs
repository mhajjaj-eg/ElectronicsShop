using ElectronicsShop.DAL.Data;
using ElectronicsShop.DAL.IRepositories;
using ElectronicsShop.Models;
using ElectronicsShop.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;


services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
})
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

services.AddLocalization(options => options.ResourcesPath = "Resources");
services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture("en-Us");
    options.AddSupportedCultures("en-US", "ar-EG");
    options.AddSupportedUICultures("en-US", "ar-EG");
    options.FallBackToParentUICultures = true;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
});

services.AddMvc()
.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null) //prevent property names from lowecasing in API response;
.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
        return factory.Create("SharedResources", assemblyName.Name);
    };
});

services.AddControllersWithViews();

var DbConn =builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(DbConn, builder => builder.MigrationsAssembly("ElectronicsShop.DAL")));

services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddAuthentication();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
