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

    public ToolBarItemViewModel[] Items { get; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public event Action<DrawingMode>? ItemSelected;
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
                if (ItemSelected is not null)
                {
                    ItemSelected(selectedItem.Mode);
                }
                MessagingCenter.Send(this, "DrawingModeChanged", selectedItem.Mode);
            }
        }
    }

    public DrawingToolBarViewModel()
    {
        moveItem = new(ThemeHelper.GetImageSource("cursor"), MoveItemCommand, DrawingMode.Move);
        drawItem = new(ThemeHelper.GetImageSource("draw"), DrawItemCommand, DrawingMode.Draw);
        eraseItem = new(ThemeHelper.GetImageSource("erase"), EraseItemCommand, DrawingMode.Erase);
        Items = new ToolBarItemViewModel[]
        {
            moveItem,
            drawItem,
            eraseItem
        };
        selectedItem = moveItem;
        App.Current!.RequestedThemeChanged += App_RequestedThemeChanged;
    }

    private void App_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        moveItem.Source = ThemeHelper.GetImageSource("cursor");
        drawItem.Source = ThemeHelper.GetImageSource("draw");
        eraseItem.Source = ThemeHelper.GetImageSource("erase");
    }

    private void DrawItemCommand()
    {
    }
    private void MoveItemCommand()
    {
    }
    private void EraseItemCommand()
    {
    }
}
