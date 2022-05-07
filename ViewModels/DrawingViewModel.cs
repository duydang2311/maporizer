using System.ComponentModel;

namespace Maporizer.ViewModels;

internal class DrawingViewModel : INotifyPropertyChanged
{
    private const float maxRatio = 200f;
    private const int maxRemainders = (int)(maxRatio / 25f);
    private int zoomRatio;

    public event PropertyChangedEventHandler PropertyChanged;

    public int ZoomRatio
    {
        get => zoomRatio;
        set
        {
            if (zoomRatio != value)
            {
                int remainder = maxRemainders - (int)Math.Round((float)(maxRatio - value) / 25f);
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
