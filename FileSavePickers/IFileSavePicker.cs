namespace Maporizer.FileSavePickers;

public interface IFileSavePicker
{
    Task<string?> PickAsync();
}
