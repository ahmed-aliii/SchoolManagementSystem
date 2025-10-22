using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.BLL;
using School.DAL;
using School.Domain;
using Serilog;

namespace School.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Enable Debug logging (visible in VS Output window)
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();  // Optional — shows logs if you run with Kestrel
            builder.Logging.AddDebug();    // Required for Visual Studio Output window


            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console() // For dotnet run / terminal output
                .WriteTo.Debug()   // For Visual Studio → Output window
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Daily log file
                .Enrich.FromLogContext() // Add context info like request path, etc.
                .CreateLogger();

            // Tell ASP.NET Core to use Serilog instead of default logger
            builder.Host.UseSerilog();


            builder.Services.AddScoped<LocationRestrictionFilter>();
            builder.Services.AddScoped<LogResultFilter>();

            builder.Services.AddScoped<CustomExceptionFilter>();

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<CacheResourceFilter>();


            builder.Services.AddDbContext<SchoolDB>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDB")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));


            #region Session Regiteration
            // Add services for Session
            builder.Services.AddDistributedMemoryCache(); // Required for session storage
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
                options.Cookie.HttpOnly = true;   // Prevent JS access
            });
            #endregion

            #region Identity Services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // -------------------
                // Password settings
                // -------------------
                options.Password.RequireDigit = false;                // Must contain a number
                options.Password.RequireLowercase = false;            // Must contain a lowercase letter
                options.Password.RequireUppercase = false;            // Must contain an uppercase letter
                options.Password.RequireNonAlphanumeric = false;     // Must contain a special character
                options.Password.RequiredLength = 4;                // Minimum length
                options.Password.RequiredUniqueChars = 0;           // Minimum unique characters


                // -------------------
                // User settings
                // -------------------
                options.User.RequireUniqueEmail = true;           // Email must be unique
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // Allowed username chars


                // -------------------
                // Lockout settings
                // -------------------
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Lockout duration
                options.Lockout.MaxFailedAccessAttempts = 5;                       // Max failed attempts
                options.Lockout.AllowedForNewUsers = true;                         // Lockout enabled for new users


                // -------------------
                // SignIn settings
                // -------------------
                options.SignIn.RequireConfirmedEmail = false;     // Require email confirmation
                options.SignIn.RequireConfirmedPhoneNumber = false; // Require phone confirmation
            })
            .AddEntityFrameworkStores<SchoolDB>();
            #endregion

            #region Cookie Service
            builder.Services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    // 🔹 Paths
                    options.LoginPath = "/Account/LogIn";   // Redirect here if not logged in
                    options.LogoutPath = "/Account/LogOut";     // Redirect here after logout
                    options.AccessDeniedPath = "/Account/AccessDenied"; // For [Authorize] failed

                    // 🔹 Cookie Settings
                    options.Cookie.Name = "Quizera";          // Cookie name
                    options.Cookie.HttpOnly = true;             // Can't be accessed by JS
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Only HTTPS
                    options.Cookie.SameSite = SameSiteMode.Strict; // Strict CSRF protection

                    // 🔹 Expiration
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie lifetime
                    options.SlidingExpiration = true;  // Refresh expiration on activity

                    // 🔹 Return URL
                    options.ReturnUrlParameter = "returnUrl"; // Query string for redirect

                });
            #endregion

            #region Add AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
            }, typeof(AccountProfile).Assembly);  // Scans Core assembly for profiles 
            #endregion


            // Add services to the container.
            builder.Services.AddControllersWithViews();

          

            var app = builder.Build();


            app.UseRequestLogging();


            // Add your middleware early in the pipeline

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {

                //Custom Exception
                app.UseGlobalExceptionHandler();

                //app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
