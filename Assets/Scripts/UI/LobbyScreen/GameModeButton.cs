using System;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;

    public GameMode GameMode => _gameMode;

    [Space(5)]
    [SerializeField] private Image _outline;
    [SerializeField] private Button _modeButton;

    public event Action<GameModeButton> ModeSelect;

    public void Initialize()
    {
        _modeButton.onClick.AddListener(SelectMode);
    }

    public void SelectMode()
    {
        ModeSelect?.Invoke(this);
    }

    public void SetModeButtonSelected(bool isSelected)
    {
        _outline.gameObject.SetActive(isSelected);
    }
}
