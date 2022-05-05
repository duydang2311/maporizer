using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Maporizer.CanvasViews
{
    public partial class CanvasView : UserControl
    {
        public CanvasView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public Canvas Canvas
        {
            get => __Canvas;
        }
    }
}