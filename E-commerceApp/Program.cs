// Whenever we have to register something, we will do that in Program.cs
using E_commerceApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//adding Entity FrameWork Core in order to use DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
// We have added dbcontext to our container.We also want to configure some option.
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
// Pipeline basically means that when
// a request comes to an application, how do you want to process that?
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
// It will configure all static files in wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Routing in MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // ? means Nullable
app.Run();
