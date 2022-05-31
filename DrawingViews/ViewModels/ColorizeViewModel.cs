using Maporizer.MainPages;
using Maporizer.Helpers;
using Maporizer.ColorizerPrompts.Models;
using Maporizer.Colorizers;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;
using Maporizer.DrawingToolBarViews.ViewModels;
using Maporizer.DrawingToolBarViews.Models;

namespace Maporizer.DrawingViews.ViewModels;

public class ColorizeViewModel
{
    private readonly IGraphicsDrawable drawable;
    private readonly DrawingView view;
    private IColorizer? colorizer;
    private Color[] colors = null!;
    public ColorizeViewModel(DrawingView view)
    {
        this.view = view;
        drawable = (IGraphicsDrawable)view.GraphicsView.Drawable;
        MessagingCenter.Subscribe<MainPage, PromptResultModel>(view, "Colorize", OnColorize);
        MessagingCenter.Subscribe<DrawingToolBarViewModel, ToolBarItemViewModel>(this, "ItemSelected", OnItemSelected);
    }
    private void OnItemSelected(DrawingToolBarViewModel sender, ToolBarItemViewModel item)
    {
        if (colorizer is not null && item.Mode != DrawingMode.Colorize)
        {
            colorizer.Stop();
        }
    }
    private void OnColorize(MainPage sender, PromptResultModel model)
    {
        if (((IGraphicsDrawable)view.GraphicsView.Drawable).Drawings.Count == 0)
        {
            return;
        }
        colorizer =
            (model.Algorithm == "Brute-force") ? new BruteForceColorizer(OnIteration, model.Delay) :
            (model.Algorithm == "Greedy") ? new GreedyColorizer(OnIteration, model.Delay) :
            (model.Algorithm == "Recursive Largest First") ? new RecursiveLargestFirstColorizer(OnIteration, model.Delay) :
            null;
        if (colorizer is null)
        {
            return;
        }
        var matrix = DrawableHelper.MakeAdjacencyMatrix(drawable);
        var length = matrix.GetLength(0);
        colors = new Color[length];
        var rand = new Random();
        for (int i = 0; i != length; ++i)
        {
            colors[i] = Color.FromRgb(rand.Next(255), rand.Next(255), rand.Next(255));
        }
        Task.Run(() =>
        {
            var result = colorizer.Colorize(matrix);
        });
    }
    private void OnIteration(int[] result)
    {
        var idx = 0;
        view.Dispatcher.Dispatch(() =>
        {
            foreach (var i in drawable.Drawings)
            {
                i.FillColor = result[idx] == -1 ? Colors.Transparent : colors[result[idx]];
                ++idx;
            }
            view.GraphicsView.Invalidate();
        });
    }
}
