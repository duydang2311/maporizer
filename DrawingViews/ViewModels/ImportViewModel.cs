using Maporizer.MainPages;
using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.Helpers;

namespace Maporizer.DrawingViews.ViewModels;

public class ImportViewModel
{
    private readonly IGraphicsDrawable drawable;
    private readonly DrawingView view;
    public ImportViewModel(DrawingView view)
    {
        this.view = view;
        drawable = (IGraphicsDrawable)view.GraphicsView.Drawable;
        MessagingCenter.Subscribe<MainPage, FileResult>(view, "Import", OnImport);
    }
    private void OnImport(MainPage sender, FileResult file)
    {
        var color = ThemeHelper.GetThemeBasedValue((Color)App.Current!.Resources["Black"], (Color)App.Current!.Resources["White"]);
        Task.Run(() =>
        {
            using var reader = new StreamReader(file.FullPath);
            var drawings = new LinkedList<IDrawableShape>(drawable.Drawings);
            int count = 0;
            lock (drawable.Drawings)
            {
                foreach (var i in drawings)
                {
                    i.Dispose();
                }
                drawable.Drawings.Clear();
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line is null)
                    {
                        continue;
                    }
                    ++count;
                    var points = line.Split(' ');
                    var polygon = new PolygonModel { StrokeColor = color };
                    for(int i = 0, length = points.Length; i < length; i += 2)
                    {
                        if (float.TryParse(points[i], out var x) && float.TryParse(points[i + 1], out var y))
                        {
                            polygon.Add(new PointF(x, y));
                        }
                    }
                    polygon.Close();
                    drawable.Draw(polygon);
                }
            }
            reader.Close();
            view.Dispatcher.Dispatch(() =>
            {
                MessagingCenter.Send(view, "Imported", count);
                view.GraphicsView.Invalidate();
            });
        });
    }
}
