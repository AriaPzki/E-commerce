// Whenever we have to register something, we will do that in Program.cs
using E_commerce.DataAccess.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
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


//  different service lifetimes in dependency injection:
//TRANSIENT : basically means that whenever we want any implementation,create a new object, and give that new object.
//You never have to reuse an existing object.Every time a service is requested,a new implementation is created.
// SCOPED: where it depends on the HTTP request. So whenever an HTTP request is sent to the server, that time for that scope,
// one lifetime will be created, and then the same object or same service is used wherever in that request the service is requested.
// So let's say for one single page load, we are calling a service 10 times. It'll only create that object one time, and it'll use that same object 10 times in that one request.
// SINGLETON : singleton, where one implementation is created for the lifetime of the application.

