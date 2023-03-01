using UrlShortener.Data.Persistence.Implementation;
using UrlShortener.Data.Persistence.Interface;
using UrlShortener.Data.Repositories.Implementation;
using UrlShortener.Data.Repositories.Interface;
using UrlShortener.Data.Services.Implementation;
using UrlShortener.Data.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(typeof(IAddressService), typeof(AddressService));
builder.Services.AddSingleton(typeof(IPersistedDataRepository), typeof(PersistedDataRepository));
builder.Services.AddSingleton(typeof(IAddressRepository), typeof(AddressRepository));
builder.Services.AddSingleton(typeof(IRandomStringGenerator), typeof(RandomStringGenerator));
builder.Services.AddSingleton(typeof(IUrlValidationService), typeof(UrlValidationService));
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
