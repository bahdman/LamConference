using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LamConference.Services;
using LamConference.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LamConference.Data.AppContext>(
    service => service.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"))
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<LamConference.Data.AppContext>();

builder.Services.AddScoped<IAccount, AccountRepository>();
builder.Services.AddScoped<IIdGenerator, ReferenceIDRepository>();
builder.Services.AddScoped<IRegistration, RegistrationRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// var test = WebApplication.CreateBuilder(Host.CreateApplicationBuilder().Environment.EnvironmentName = "Production");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Home/HttpStatusCodeHandler");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //Big TODO:: change back to home index when home page is ready
    pattern: "{controller=Registration}/{action=Register}/{id?}");

app.Run();
