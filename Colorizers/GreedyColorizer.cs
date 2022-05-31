namespace Maporizer.Colorizers;

public class GreedyColorizer : Colorizer
{
    public GreedyColorizer(Action<int[]> postIteration, uint iterationDelay) : base(postIteration, iterationDelay) { }
    public override int[] Colorize(bool[,] matrix)
    {
        var length = matrix.GetLength(0);
        var result = new int[length];
        var available = new bool[length];

        result[0] = 0;
        for (int i = 1; i != length; ++i)
            result[i] = -1;

        for (int u = 1; u != length; u++)
        {
            for (int i = 0; i != length; ++i)
            {
                if (matrix[u, i] && result[i] != -1)
                {
                    available[result[i]] = true;
                }
            }

            int cr;
            for (cr = 0; cr != length; cr++)
            {
                if (available[cr] == false)
                {
                    break;
                }
            }

            result[u] = cr;
            PostIterationInternal(result);
            for (int i = 0; i != length; ++i)
            {
                if (matrix[u, i] && result[i] != -1)
                {
                    available[result[i]] = false;
                }
            }
        }
        return result;
    }
}

