using Maporizer.DrawingToolBarViews.ViewModels;

namespace Maporizer.MainPages.ViewModels.Colorizer;

public partial class ColorizerPromptViewModel
{
    private readonly ContentPage page;
    public ColorizerPromptViewModel(ContentPage page)
    {
        this.page = page;
        InitToolBarItemSelectedInternal();
    }
}
