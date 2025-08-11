using POS.Presentation.Services;
using POS.Presentation.Services.Implementations;
using POS.Presentation.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient<UserService>(client =>
//{
//    client.BaseAddress = new Uri("http://localhost:5111/"); // Your API base URL
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

//builder.Services.AddHttpClient<RoleService>(client =>
//{
//    client.BaseAddress = new Uri("http://localhost:5111/"); // Your API base URL
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

string apiUrl = builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost/"; // Default to localhost if not set
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    // Add other default headers or configurations here
});


builder.Services.AddTransient<IRoleService,RoleService>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<IMenuService, MenuService>();
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

app.Run();
