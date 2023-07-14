using static Microsoft.Maui.ApplicationModel.Permissions;
using APPEVENTOS.Services;
using Camera.MAUI;
using Microsoft.Extensions.Logging;

namespace APPEVENTOS;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCameraView() // Add the use of the plugging
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddScoped<IServicioApi, ServicioApi>();
		string dbPath = FileAccessHelper.GetLocalFilePath("carrito.db3");
		builder.Services.AddSingleton<CarritoDb>(s => ActivatorUtilities.CreateInstance<CarritoDb>(s, dbPath));
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
	}
}
