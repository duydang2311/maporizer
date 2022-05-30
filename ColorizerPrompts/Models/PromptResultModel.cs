namespace Maporizer.ColorizerPrompts.Models;

public class PromptResultModel
{
    public string Algorithm { get; }
    public uint Delay { get; }
    public PromptResultModel(string algorithm, uint delay)
    {
        Algorithm = algorithm;
        Delay = delay;
    }
}
