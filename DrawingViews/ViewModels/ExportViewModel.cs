using Maporizer.MainPages;
using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews.ViewModels;

public class ExportViewModel
{
    private readonly IGraphicsDrawable drawable;
    private readonly DrawingView view;
    public ExportViewModel(DrawingView view)
    {
        this.view = view;
        drawable = (IGraphicsDrawable)view.GraphicsView.Drawable;
        System.Diagnostics.Debug.WriteLine("ExportViewModel subscribed");
        MessagingCenter.Subscribe<MainPage, string>(view, "Export", OnExport);
    }
    private async void OnExport(MainPage sender, string path)
    {
        await Task.Run(() =>
        {
            var drawings = new LinkedList<IDrawableShape>(drawable.Drawings);
            using var file = new StreamWriter(path, append: false);
            foreach (PolygonModel i in drawings)
            {
                foreach (var p in i.Path.Points)
                {
                    file.Write($"{p.X} {p.Y} ");
                    file.Flush();
                }
                file.WriteLine();
            }
            file.Close();
        });
        MessagingCenter.Send(view, "Exported");
    }
}
