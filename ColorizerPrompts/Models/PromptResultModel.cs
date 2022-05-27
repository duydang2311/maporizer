namespace Maporizer.ColorizerPrompts.Models;

public class PromptResultModel
{
    public string Algorithm { get; }
    public int Colors { get; }
    public PromptResultModel(string algorithm, int colors)
    {
        Algorithm = algorithm;
        Colors = colors;
    }
}
