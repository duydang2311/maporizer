using System.ComponentModel;
using Maporizer.Helpers;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingToolBarViews.ViewModels;

public class DrawingToolBarViewModel : INotifyPropertyChanged
{
    private ToolBarItemViewModel selectedItem;
    private readonly ToolBarItemViewModel drawItem;
    private readonly ToolBarItemViewModel moveItem;

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
            }
        }
    }

    public DrawingToolBarViewModel()
    {
        drawItem = new(ThemeHelper.GetImageSource("draw"), DrawItemCommand, DrawingMode.Draw);
        moveItem = new(ThemeHelper.GetImageSource("cursor"), MoveItemCommand, DrawingMode.Move);
        Items = new ToolBarItemViewModel[]
        {
            drawItem,
            moveItem
        };
        selectedItem = moveItem;
    }
    private void DrawItemCommand()
    {
    }
    private void MoveItemCommand()
    {
    }
}
