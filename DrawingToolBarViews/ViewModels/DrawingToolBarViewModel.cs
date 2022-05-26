using System.ComponentModel;
using Maporizer.Helpers;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingToolBarViews.ViewModels;

public class DrawingToolBarViewModel : INotifyPropertyChanged
{
    private ToolBarItemViewModel selectedItem;
    private readonly ToolBarItemViewModel moveItem;
    private readonly ToolBarItemViewModel drawItem;
    private readonly ToolBarItemViewModel eraseItem;
    private readonly ToolBarItemViewModel colorizeItem;

    public ToolBarItemViewModel[] Items { get; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ToolBarItemViewModel SelectedItem
    {
        get => selectedItem;
        set
        {
            if (selectedItem != value)
            {
                selectedItem = value;
                if (PropertyChanged is not null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
                }
                MessagingCenter.Send(this, "ItemSelected", selectedItem);
            }
        }
    }

    public DrawingToolBarViewModel()
    {
        moveItem = new(ThemeHelper.GetImageSource("cursor"), DrawingMode.Move);
        drawItem = new(ThemeHelper.GetImageSource("draw"), DrawingMode.Draw);
        eraseItem = new(ThemeHelper.GetImageSource("erase"), DrawingMode.Erase);
        colorizeItem = new(ThemeHelper.GetImageSource("colorize"), DrawingMode.Colorize);
        Items = new ToolBarItemViewModel[]
        {
            moveItem,
            drawItem,
            eraseItem,
            colorizeItem
        };
        selectedItem = moveItem;
        App.Current!.RequestedThemeChanged += App_RequestedThemeChanged;
    }

    private void App_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        moveItem.Source = ThemeHelper.GetImageSource("cursor");
        drawItem.Source = ThemeHelper.GetImageSource("draw");
        eraseItem.Source = ThemeHelper.GetImageSource("erase");
        colorizeItem.Source = ThemeHelper.GetImageSource("colorize");
    }
}
