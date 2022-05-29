using Maporizer.DrawingViews.Models;
using Maporizer.DrawingViews.Models.GraphicsDrawableModels;

namespace Maporizer.Helpers;

public static class DrawableHelper
{
    public static bool[,] MakeAdjacencyMatrix(IGraphicsDrawable drawable)
    {
        var length = drawable.Drawings.Count;
        var drawings = new IDrawableShape[length];
        var matrix = new bool[length, length];
        drawable.Drawings.CopyTo(drawings, 0);
        for (int i = 0, offset = length - 1; i != offset; ++i)
        {
            for (int j = i + 1; j != length; ++j)
            {
                foreach (var p in drawings[j].Path.Points)
                {
                    if (drawings[i].HasPointOn(p) || drawings[i].HasPointIn(p))
                    {
                        matrix[i, j] = matrix[j, i] = true;
                        break;
                    }
                }
            }
        }
        return matrix;
    }
}
