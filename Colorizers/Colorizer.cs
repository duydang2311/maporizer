namespace Maporizer.Colorizers;

public abstract class Colorizer : IColorizer
{
    private readonly Action<int[]> postIteration;
    public int IterationDelay { get; set; }
    public Colorizer(Action<int[]> postIteration, int iterationDelay)
    {
        this.postIteration = postIteration;
        IterationDelay = iterationDelay;
    }
    public abstract int[] Colorize(bool[,] matrix, int colors);
    protected void PostIterationInternal(int[] result)
    {
        Thread.Sleep(IterationDelay);
        postIteration(result);
    }
}
