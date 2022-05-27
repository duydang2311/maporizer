using Maporizer.ColorizerPrompts.ViewModels;

namespace Maporizer.ColorizerPrompts;

public partial class ColorizerPromptPopup
{
    private void InitCancelInternal()
    {
        MessagingCenter.Subscribe<PromptViewModel>(this, "Cancel", OnCancel);
    }
    private void OnCancel(PromptViewModel sender)
    {
        Close();
    }
}
