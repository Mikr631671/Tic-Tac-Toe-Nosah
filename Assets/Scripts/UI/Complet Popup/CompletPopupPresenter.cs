using Cysharp.Threading.Tasks;

public class CompletPopupPresenter
{
    private readonly CompletPopupModel model;
    private readonly CompletPopupView view;

    public CompletPopupPresenter(CompletPopupModel model, CompletPopupView view)
    {
        this.model = model;
        this.view = view;
    }

    public async UniTask ShowPopupAsync()
    {
        // Get the formatted title from the model
        string formattedTitle = model.GetFormattedTitle();

        // Show the popup with the provided title
        view.ShowPopup(formattedTitle);

        // Wait for the popup to close
        await view.WaitForCloseAsync();
    }
}
