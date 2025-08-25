using WebApStudentEnrolment.Models;                                             // For accessing model classes
using WebApStudentEnrolment.Data;                                               // For accessing the DbContext
using Microsoft.EntityFrameworkCore;                                            // For Entity Framework Core functionality
using WebApStudentEnrolment.Repositories;                                       // For accessing repositories

// Create a builder object to configure and build the web application
var builder = WebApplication.CreateBuilder(args);

// Add MVC controllers and views to the services container
builder.Services.AddControllersWithViews();

// Register the EF Core DbContext and configure it to use SQL Server
builder.Services.AddDbContext<StudentEnrolmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnrolmentConnection")));

// Optional: (though AddControllersWithViews already adds MVC)
builder.Services.AddMvc();

/* Register Repositories using Dependency Injection */

// Register IStudent interface with its implementation StudentRepo
builder.Services.AddTransient<IStudent, StudentRepo>();

// Register ICourse interface with its implementation CourseRepo
builder.Services.AddTransient<ICourse, CourseRepo>();

// Register IEnrolments interface with its implementation EnrolmentRepo
builder.Services.AddTransient<IEnrolments, EnrolmentRepo>();

// Build the application (compile the above service configurations)
var app = builder.Build();

// Use custom error page if the app is not in development mode
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");                                     // Redirects to /Home/Error on exceptions
    app.UseHsts();                                                              // Enables HTTP Strict Transport Security
}

app.UseHttpsRedirection();                                                      // Redirect all HTTP requests to HTTPS

app.UseStaticFiles();                                                           // Serve static files (CSS, JS, images, etc.)

app.UseRouting();                                                               // Enable routing in the application

app.UseAuthorization();                                                         // Use authorization middleware (even if unused yet)

// Define the default route pattern: Controller = Home, Action = Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();                                                                      // Start the application and begin listening for HTTP requests