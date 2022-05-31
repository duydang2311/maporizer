namespace Maporizer.Colorizers;

public class RecursiveLargestFirstColorizer : Colorizer
{
    private bool[,] graph = null!;
    private int[] NN = null!;
    private int[] result = null!;
    private int[] degree = null!;
    private int NNCount;
    private int unprocessed;
    private int V;
    public RecursiveLargestFirstColorizer(Action<int[]> postIteration, uint iterationDelay) : base(postIteration, iterationDelay)
    {
    }
    public override int[] Colorize(bool[,] matrix)
    {
        graph = matrix;
        V = graph.GetLength(0);
        var unprocessed = matrix.GetLength(0);
        result = new int[unprocessed];
        degree = new int[unprocessed];
        NN = new int[unprocessed];
        Initiate();
        Coloring();
        return result;
    }
    private void Initiate()
    {
        for (int i = 0; i < V; i++)
        {
            result[i] = 0;
            degree[i] = 0;
            for (int j = 0; j < V; j++)
                if (graph[i, j] == true)
                    degree[i]++;
        }
        NNCount = 0;
        unprocessed = V;
    }

    private int MaxDegreeVertex()
    {
        int max = -1;
        int max_i = 0;
        for (int i = 0; i < V; ++i)
            if (result[i] == 0)
                if (degree[i] > max)
                {
                    max = degree[i];
                    max_i = i;
                }
        return max_i;
    }

    private void scannedInitiate(int[] scanned)
    {
        for (int i = 0; i < V; i++)
            scanned[i] = 0;
    }

    private void UpdateNN(int ColorNumber)
    {
        NNCount = 0;
        for (int i = 0; i < V; ++i)
            if (result[i] == 0)
            {
                NN[NNCount] = i;
                NNCount++;
            }

        for (int i = 0; i < V; i++)
            if (result[i] == ColorNumber)
                for (int j = 0; j < NNCount; j++)
                    while (graph[i, NN[j]] == true && NNCount != 0)
                    {
                        NN[j] = NN[NNCount - 1];
                        NNCount--;
                    }
    }

    private int FindSuitableY(int ColorNumber, ref int VerticesInCommon)
    {
        int temp = 0, tmp_y = 0, y = 0;
        int[] scanned = new int[V];
        VerticesInCommon = 0;
        for (int i = 0; i < NNCount; i++)
        {
            tmp_y = NN[i];
            temp = 0;
            scannedInitiate(scanned);

            for (int x = 0; x < V; x++)
                if (result[x] == ColorNumber)
                    for (int k = 0; k < V; k++)
                        if (result[k] == 0 && scanned[k] == 0)
                            if (graph[x, k] == true && graph[tmp_y, k] == true)
                            {
                                temp++;
                                scanned[k] = 1;
                            }
            if (temp > VerticesInCommon)
            {
                VerticesInCommon = temp;
                y = tmp_y;
            }
        }
        return y;
    }

    private int MaxDegreeInNN()
    {
        int tmp_y = 0;
        int temp = 0, max = 0;
        for (int i = 0; i < NNCount; i++)
        {
            temp = 0;
            for (int j = 0; j < V; j++)
                if (result[j] == 0 && graph[NN[i], j] == true)
                    temp++;
            if (temp > max)
            {
                max = temp;
                tmp_y = NN[i];
            }
        }
        if (max == 0)
            return NN[0];
        else
            return tmp_y;
    }

    private void Coloring()
    {
        int x, y;
        int ColorNumber = -1;
        int VerticesInCommon = 0;
        while (unprocessed > 0)
        {
            x = MaxDegreeVertex();
            ColorNumber++;
            result[x] = ColorNumber;
            PostIterationInternal(result);
            if (State == ColorizerState.Stopped)
            {
                break;
            }
            else while (State == ColorizerState.Paused);
            unprocessed--;
            UpdateNN(ColorNumber);
            while (NNCount > 0)
            {
                y = FindSuitableY(ColorNumber, ref VerticesInCommon);
                if (VerticesInCommon == 0)
                    y = MaxDegreeInNN();
                result[y] = ColorNumber;
                PostIterationInternal(result);
                if (State == ColorizerState.Stopped)
                {
                    break;
                }
                else while (State == ColorizerState.Paused);
                unprocessed--;
                UpdateNN(ColorNumber);
            }
        }
    }
}
