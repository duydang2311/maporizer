using CommunityToolkit.Maui;
using Maporizer.FileSavePickers;

namespace Maporizer;

public static class MauiProgram
{
	public static ServiceProvider ServiceProvider { get; private set; } = null!;
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Inter-Regular.ttf", "InterRegular");
				fonts.AddFont("Inter-Bold.ttf", "InterBold");
				fonts.AddFont("Inter-Light.ttf", "InterLight");
			})
			.UseMauiCommunityToolkit();
#if WINDOWS
        builder.Services.AddTransient<IFileSavePicker, Platforms.Windows.FileSavePicker>();
#elif MACCATALYST
		builder.Services.AddTransient<IFileSavePicker, Platforms.MacCatalyst.FileSavePicker>();
#endif
		ServiceProvider = builder.Services.BuildServiceProvider();
        return builder.Build();
	}
}
