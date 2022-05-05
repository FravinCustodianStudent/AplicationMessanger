using AplicationMessanger.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AplicationMessanger.Data;
using AplicationMessanger.Models.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
//_____________________________________________________________________________________________________________________________________
var connectionString = builder.Configuration.GetConnectionString("AplicationMessangerContextConnection"); ;

builder.Services.AddDbContext<AplicationMessangerContext>(options =>
    options.UseSqlServer(connectionString)); ;

builder.Services.AddDefaultIdentity<AplicationMessangerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AplicationMessangerContext>(); ;

builder.Services.AddScoped<Message>();
builder.Services.AddScoped<Chat>();



builder.Services.AddRazorPages();
//builder.Services.AddAuthorization(options =>
//{
//    // This says, that all pages need AUTHORIZATION. But when a controller, 
//    // for example the login controller in Login.cshtml.cs, is tagged with
//    // [AllowAnonymous] then it is not in need of AUTHORIZATION. 
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
//{
//    options.LoginPath = "/Areas/Identity/Pages/Login";
//});



builder.Services.AddControllersWithViews();



var app = builder.Build();

//_____________________________________________________________________________________________________________________________________
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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
