using Maporizer.DrawingToolBarViews.Models;
using System.ComponentModel;

namespace Maporizer.DrawingToolBarViews.ViewModels;

public class DrawingToolBarViewModel : INotifyPropertyChanged
{
    public ToolBarItemModel[] Items { get; set; }
    private ToolBarItemModel selectedItem;
    private readonly ToolBarItemModel createItem;
    private readonly ToolBarItemModel connectItem;

    public ToolBarItemModel SelectedItem
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
    public event PropertyChangedEventHandler PropertyChanged;

    public DrawingToolBarViewModel()
    {
        createItem = new() { Source = ImageSource.FromFile("plus_light.png") };
        connectItem = new() { Source = ImageSource.FromFile("connect_light.png") };
        Items = new ToolBarItemModel[]
        {
            createItem,
            connectItem
        };
        SelectedItem = createItem;
    }
}
