using System.ComponentModel;

namespace Maporizer.ViewModels;

internal class DrawingViewModel : INotifyPropertyChanged
{
    private const float zoomUnit = 25f;
    private int zoomRatio;

    public event PropertyChangedEventHandler PropertyChanged;

    public int ZoomRatio
    {
        get => zoomRatio;
        set
        {
            if (zoomRatio != value)
            {
                int remainder = (int)Math.Round(value / zoomUnit);
                zoomRatio = remainder * 25;
                if (PropertyChanged is not null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ZoomRatio)));
                }
            }
        }
    }
    public DrawingViewModel()
    {
        zoomRatio = 100;
    }
}
