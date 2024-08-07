using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Team_5.Context;
using Team_5.Services;
using Team_5.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


//CONNECTION - DATACONTEXT
var conn = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(conn));


//AUTH
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Register";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });


// POLICIES
builder.Services.AddAuthorization(options =>
{
    // Master Policy
    options.AddPolicy("MasterPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "master"); // [Authorize(Policy = "MasterPolicy")]
    });

    // Admin Policy
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "admin"); // [Authorize(Policy = "AdminPolicy")]
    });

});


//SERVICES
builder.Services
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IMasterService, MasterService>()
    .AddScoped<IAnimalsService, AnimalsService>()
    .AddScoped<IBreedsService, BreedsService>()
    .AddScoped<IExaminationService, ExaminationService>()
    .AddScoped<IOwnerService, OwnerService>()
    .AddScoped<IHospitalizationService, HospitalizationService>()
    .AddScoped<IProductService, ProductService>();
//other services

// Add services to the container.
builder.Services.AddControllersWithViews();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

