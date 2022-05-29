using Maporizer.MainPages;
using Maporizer.Helpers;
using Maporizer.ColorizerPrompts.Models;
using Maporizer.Colorizers;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.DrawingViews.ViewModels;

public class ColorizeViewModel
{
    private readonly IGraphicsDrawable drawable;
    private readonly DrawingView view;
    public ColorizeViewModel(DrawingView view)
    {
        this.view = view;
        drawable = (IGraphicsDrawable)view.GraphicsView.Drawable;
        MessagingCenter.Subscribe<MainPage, PromptResultModel>(view, "Colorize", OnColorize);
    }
    private void OnColorize(MainPage sender, PromptResultModel model)
    {
        if (model.Algorithm == "Brute-force")
        {
            Task.Run(() =>
            {
                var matrix = DrawableHelper.MakeAdjacencyMatrix(drawable);
                var colorizer = new BruteForceColorizer(OnIteration, 100);
                var result = colorizer.Colorize(matrix, model.Colors);
            });
        }
    }
    private void OnIteration(int[] result)
    {
        var idx = 0;
        var colors = new Color[]
        {
            Colors.Transparent,
            Colors.Green,
            Colors.Yellow,
            Colors.Red,
            Colors.Pink,
            Colors.Purple,
            Colors.Black,
            Colors.Brown,
            Colors.DarkCyan,
            Colors.Cyan,
            Colors.Tomato,
            Colors.Orange
        };
        view.Dispatcher.Dispatch(() =>
        {
            foreach (var i in drawable.Drawings)
            {
                i.FillColor = colors[result[idx++] + 1];
            }
            view.GraphicsView.Invalidate();
        });
    }
}
