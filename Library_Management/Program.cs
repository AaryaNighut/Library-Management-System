using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 262144000; // 250 MB in bytes
});
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Gryphos_AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();
