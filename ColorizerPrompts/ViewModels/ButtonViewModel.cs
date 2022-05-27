namespace Maporizer.ColorizerPrompts.ViewModels;

public class PromptViewModel
{
    public Command OKCommand { get; }
    public Command CancelCommand { get; }
    public PromptViewModel()
    {
        OKCommand = new Command(OKCommandHandler, OKCanExecuteHandler);
        CancelCommand = new Command(CancelCommandHandler);
    }
    private void OKCommandHandler()
    {
        MessagingCenter.Send(this, "OK");
    }
    private void CancelCommandHandler()
    {
        MessagingCenter.Send(this, "Cancel");
    }
    private bool OKCanExecuteHandler()
    {
        return true;
    }
}
