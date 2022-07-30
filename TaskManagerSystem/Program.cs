using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.DAL.Context;
using TaskManagerSystem.DAL.Repositories.Concrete;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*1*/
builder.Services.AddDbContext<ProjectContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
options.UseLazyLoadingProxies(true);
});

/*2*/
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ProjectContext>();

/*3*/
builder.Services.ConfigureApplicationCookie(a =>
{
a.LoginPath = new PathString("/Home/Index");
a.ExpireTimeSpan = TimeSpan.FromDays(1);
a.Cookie = new CookieBuilder { Name = "KullaniciCokie", SecurePolicy = CookieSecurePolicy.Always };
});

builder.Services.AddScoped<ICompanyRepository, CompanyRespository>();
builder.Services.AddScoped<IManagerUserRepository, ManagerUserRepository>();
builder.Services.AddScoped<ITaskDescriptionRespository, TaskDescriptionRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();

builder.Services.AddAutoMapper(typeof(Mapping));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
  name: "area",
  areaName: "Admin",
  pattern: "{area:exists}/{controller}/{action}/{id?}"
);
app.MapAreaControllerRoute(
  name: "area",
  areaName: "Manager",
  pattern: "{area:exists}/{controller}/{action}/{id?}"
);
app.MapAreaControllerRoute(
  name: "area",
  areaName: "Personel",
  pattern: "{area:exists}/{controller}/{action}/{id?}"
);


/*DataSeed için*/
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
