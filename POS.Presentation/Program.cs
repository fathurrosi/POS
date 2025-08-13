using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using POS.Presentation.Services;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;
using POS.Shared.Handlers;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiBaseUrl");

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    // Add other default headers or configurations here
});


builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMenuService, MenuService>();

builder.Services.AddAuthentication(options =>
{
   
    options.DefaultScheme = POS.Shared.Constants.Cookies_Name;
    options.DefaultChallengeScheme = POS.Shared.Constants.Cookies_Name;
})
.AddCookie(POS.Shared.Constants.Cookies_Name, options =>
{
    // Cookie settings
    options.LoginPath = "/User/Login";
    options.EventsType = typeof(POSCookieHandler);    
    options.Cookie.HttpOnly = true; // Prevents client-side script access to the cookie
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1); // Sets the cookie expiration time
    options.LoginPath = "/User/Login"; // Path to the login page
    options.LogoutPath = "/Home/SignOut";
    options.AccessDeniedPath = "/Home/AccessDenied"; // Path for access denied
    options.SlidingExpiration = true; // Renews the cookie if it's nearing expiration and the user is active
    options.Cookie.Name = POS.Shared.Constants.Cookies_Name; // Optional: Custom cookie name
});


builder.Services.AddScoped<POSCookieHandler>();
//builder.Services.AddTransient<POSCookieHandler>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Information/Error");
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
