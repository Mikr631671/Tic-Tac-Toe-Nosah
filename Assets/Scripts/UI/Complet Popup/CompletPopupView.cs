using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompletPopupView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button restartButton;

    private UniTaskCompletionSource<bool> _popupCloseCompletionSource;

    public void ShowPopup(string formattedTitle)
    {
        gameObject.SetActive(true);
        title.text = formattedTitle;

        // Prepare a UniTaskCompletionSource to await popup closure
        _popupCloseCompletionSource = new UniTaskCompletionSource<bool>();
    }

    public UniTask WaitForCloseAsync()
    {
        return _popupCloseCompletionSource.Task;
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        _popupCloseCompletionSource?.TrySetResult(true);
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(ClosePopup);
    }
}