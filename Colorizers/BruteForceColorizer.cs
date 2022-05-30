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
        var colors = 5;
        if (ColorizeUtil(matrix, 0, result))
        {
            var max = result.Max();
            if (max < colors - 1)
            {
                var temp = colors - max - 1;
                while (temp != 0)
                {
                    bool check = false;
                    for (int i = 0, offset = length - 1; i != offset; ++i)
                    {
                        for (int j = i + 1; j != length; ++j)
                        {
                            if (result[i] == result[j])
                            {
                                if (State == ColorizerState.Stopped)
                                {
                                    break;
                                }
                                else while (State == ColorizerState.Paused);
                                result[i] = result.Max() + 1;
                                PostIterationInternal(result);
                                check = true;
                                break;
                            }
                        }
                        if (check == true) break;
                    }
                    temp--;
                }
            }
        }
        else
        {
            for (int i = 0; i != length; ++i)
            {
                result[i] = -1;
            }
        }
        Stop();
        return result;
    }
    private bool ColorizeUtil(bool[,] matrix, int i, int[] result)
    {
        var colors = 5;
        PostIterationInternal(result);
        if (i == matrix.GetLength(0))
        {
            return IsSafe(matrix, result);
        }
        for (int j = 0; j != colors; ++j)
        {
            result[i] = j;
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

