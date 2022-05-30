namespace Maporizer.Colorizers;

public abstract class Colorizer : IColorizer
{
    private readonly Action<int[]> postIteration;
    private ColorizerState state;
    public ColorizerState State
    {
        get
        {
            lock(mutex)
            {
                return state;
            }
        }
        private set
        {
            lock(mutex)
            {
                state = value;
            }
        }
    }
    private readonly object mutex = new();
    public uint IterationDelay { get; set; }
    public Colorizer(Action<int[]> postIteration, uint iterationDelay)
    {
        this.postIteration = postIteration;
        IterationDelay = iterationDelay;
    }
    public abstract int[] Colorize(bool[,] matrix);
    protected void PostIterationInternal(int[] result)
    {
        Thread.Sleep((int)IterationDelay);
        postIteration(result);
    }
    public void Stop()
    {
        State = ColorizerState.Stopped;
    }
    public void Resume()
    {
        State = ColorizerState.Running;
    }
    public void Pause()
    {
        State = ColorizerState.Paused;
    }
}
