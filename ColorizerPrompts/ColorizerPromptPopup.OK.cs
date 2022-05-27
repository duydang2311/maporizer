using Maporizer.ColorizerPrompts.Models;
using Maporizer.ColorizerPrompts.ViewModels;

namespace Maporizer.ColorizerPrompts;

public partial class ColorizerPromptPopup
{
    private void InitOKInternal()
    {
        MessagingCenter.Subscribe<PromptViewModel, PromptResultModel>(this, "OK", OnOK);
    }
    private void OnOK(PromptViewModel sender, PromptResultModel model)
    {
        Close(model);
    }
}
