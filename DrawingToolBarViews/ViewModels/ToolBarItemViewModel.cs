using System.ComponentModel;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingToolBarViews.ViewModels;

public class ToolBarItemViewModel : INotifyPropertyChanged
{
    private ImageSource source;
    public Command Command { get; }
    public DrawingMode Mode { get; }

    public ImageSource Source
    {
        get => source;
        set
        {
            if (source != value)
            {
                source = value;
                if (PropertyChanged is not null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Source)));
                }
            }
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ToolBarItemViewModel(ImageSource source, Action execute, DrawingMode mode)
    {
        this.source = source;
        Mode = mode;
        Command = new Command(execute);
    }
}
