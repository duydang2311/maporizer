using System.ComponentModel;
using Maporizer.Helpers;

namespace Maporizer.DrawingToolBarViews.ViewModels;

public class DrawingToolBarViewModel : INotifyPropertyChanged
{
    public ToolBarItemViewModel[] Items { get; }
    private ToolBarItemViewModel selectedItem;
    private readonly ToolBarItemViewModel drawItem;
    private readonly ToolBarItemViewModel moveItem;

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
            }
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public DrawingToolBarViewModel()
    {
        drawItem = new(ThemeHelper.GetImageSource("draw"), DrawItemCommand);
        moveItem = new(ThemeHelper.GetImageSource("cursor"), MoveItemCommand);
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
