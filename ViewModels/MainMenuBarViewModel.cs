namespace Maporizer.ViewModels;

public static class MainMenuBarViewModel
{
    private static readonly MenuBarItem file = new MenuBarItem { Text = "File" };
    private static readonly MenuFlyoutItem file_new = new MenuFlyoutItem { Text = "New", Command = file_newCommand };

    private static readonly MenuBarItem edit = new MenuBarItem { Text = "Edit" };

    private static readonly MenuBarItem view = new MenuBarItem { Text = "View" };

    private static readonly MenuBarItem help = new MenuBarItem { Text = "Help" };

    private static readonly Command file_newCommand = new Command(newCommandHandler);

    public static void InitMainMenuBar(ContentPage page)
    {
        page.MenuBarItems.Clear();

        page.MenuBarItems.Add(file);
        page.MenuBarItems.Add(edit);
        page.MenuBarItems.Add(view);
        page.MenuBarItems.Add(help);

        file.Add(file_new);
    }

    private static void newCommandHandler(object sender)
    {
    }
}
