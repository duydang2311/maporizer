namespace Maporizer.Colorizers;

public class BruteForceColorizer : Colorizer
{
    public BruteForceColorizer(Action<int[]> postIteration, uint iterationDelay) : base(postIteration, iterationDelay) { }
    public override int[] Colorize(bool[,] matrix)
    {
        var length = matrix.GetLength(0);
        var result = new int[length];
        for (int i = 0; i != length; ++i)
        {
            result[i] = -1;
        }
        ColorizeUtil(matrix, 0, result);
        Stop();
        return result;
    }
    private bool ColorizeUtil(bool[,] matrix, int i, int[] result)
    {
        var length = matrix.GetLength(0);
        if (i == length)
        {
            return IsSafe(matrix, result);
        }
        for (int j = 0; j != length; ++j)
        {
            result[i] = j;
            PostIterationInternal(result);
            if (State == ColorizerState.Stopped)
            {
                break;
            }
            else while (State == ColorizerState.Paused);
            if (ColorizeUtil(matrix, i + 1, result))
                return true;
            result[i] = 0;
        }
        return false;
    }
    private static bool IsSafe(bool[,] matrix, int[] result)
    {
        var length = matrix.GetLength(0);
        for (int i = 0, offset = length - 1; i != offset; ++i)
            for (int j = i + 1; j != length; ++j)
                if (matrix[i, j] && result[j] == result[i])
                    return false;
        return true;
    }
}

