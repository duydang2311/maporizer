namespace Maporizer.Colorizers;

public enum ColorizerState : byte
{
    Running,
    Paused,
    Stopped
}

public interface IColorizer
{
    ColorizerState State { get; }
    int[] Colorize(bool[,] matrix);
    void Stop();
    void Pause();
    void Resume();
}
