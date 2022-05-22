using System.ComponentModel;

namespace Maporizer.DrawingToolBarViews.ViewModels;

public class ToolBarItemViewModel : INotifyPropertyChanged
{
    private ImageSource source;
    public Command Command { get; }

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
    public ToolBarItemViewModel(ImageSource source, Action execute)
    {
        this.source = source;
        Command = new Command(execute);
    }
}
