using Microsoft.Extensions.Logging;
using board_game_library.Data;
using Microsoft.EntityFrameworkCore;

namespace board_game_library
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            var connectionString =
                "Server=localhost;Database=BoardGameLibrary;Trusted_Connection=True;TrustServerCertificate=True;";

            builder.Services.AddDbContext<BoardGameLibraryDbContext>(options =>
                options.UseSqlServer(connectionString));

            return builder.Build();
        }
    }
}
