using Maporizer.MainPages;

namespace Maporizer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
