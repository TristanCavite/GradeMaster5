using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GradeMaster5.Services;
using Microsoft.EntityFrameworkCore;
using UraniumUI; // Ensure this is necessary for your project
using GradeMaster5.ViewModels;
namespace GradeMaster5
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services for dependency injection
            ConfigureServices(builder.Services);

            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            string dbPath = "Data Source=GradeMaster5.db";
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(dbPath));

            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<DatabaseInitializer>();
            services.AddTransient<LoginViewModel>(); // Register LoginViewModel for DI
        }
    }

    // A simple database initializer class to create the database if it does not exist
    public class DatabaseInitializer
    {
        private readonly AppDbContext _dbContext;

        public DatabaseInitializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
        }
    }
}
