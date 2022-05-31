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
        MessagingCenter.Subscribe<MainPage, string>(view, "Import", OnImport);
    }
    private void OnImport(MainPage sender, string path)
    {
        var color = ThemeHelper.GetThemeBasedValue((Color)App.Current!.Resources["Black"], (Color)App.Current!.Resources["White"]);
        Task.Run(async () =>
        {
            using var reader = !File.Exists(path)
                ? new StreamReader(await FileSystem.OpenAppPackageFileAsync(path))
                : new StreamReader(path)
            ;
            if (reader is null)
            {
                return;
            }
            var drawings = new LinkedList<IDrawableShape>(drawable.Drawings);
            int count = 0;
            lock (drawable.Drawings)
            {
                drawable.Clear();
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
            view.Dispatcher.Dispatch(() =>
            {
                MessagingCenter.Send(view, "Alert", $"Imported a map with {count} drawings");
                view.GraphicsView.Invalidate();
            });
        });
    }
}
