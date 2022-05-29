namespace Maporizer.Colorizers;

public interface IColorizer
{
    int[] Colorize(bool[,] matrix, int colors);
}
