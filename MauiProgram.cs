using CommunityToolkit.Maui;

namespace Maporizer;

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
				fonts.AddFont("Inter-Regular.ttf", "InterRegular");
				fonts.AddFont("Inter-Bold.ttf", "InterBold");
				fonts.AddFont("Inter-Light.ttf", "InterLight");
			})
			.UseMauiCommunityToolkit();

		return builder.Build();
	}
}
