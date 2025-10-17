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
