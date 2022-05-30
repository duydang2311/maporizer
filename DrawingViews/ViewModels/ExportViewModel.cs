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
        MessagingCenter.Subscribe<MainPage, string>(view, "Export", OnExport);
    }
    private void OnExport(MainPage sender, string path)
    {
        Task.Run(() =>
        {
            var drawings = new LinkedList<IDrawableShape>(drawable.Drawings);
            var count = drawings.Count;
            using var file = new StreamWriter(path, append: false);
            foreach (var i in drawings)
            {
                foreach (var p in i.Path.Points)
                {
                    file.Write($"{p.X} {p.Y} ");
                    file.Flush();
                }
                file.WriteLine();
            }
            file.Close();
            view.Dispatcher.Dispatch(() =>
            {
                MessagingCenter.Send(view, "Alert", $"Exported a map with {count} drawings");
            });
        });
    }
}
