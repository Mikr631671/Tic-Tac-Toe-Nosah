using System;

using UnityEngine;
using UnityEngine.UI;

public class LobbyScreenView : MonoBehaviour
{
    [SerializeField] private GameModeSelector _gameModeSelector;
    [SerializeField] private Button _playButton;

    public event Action<GameMode> GameModeSelected;
    public event Action GameStartButtonClicked;

    private void Start()
    {
        _gameModeSelector.Initialize();
        _gameModeSelector.ModeUpdate += OnModeUpdate;
        _playButton.onClick.AddListener(() => GameStartButtonClicked?.Invoke());
    }

    public void OnModeUpdate(GameMode gameMode)
    {
        GameModeSelected?.Invoke(gameMode);
    }

    private void OnDestroy()
    {
        _gameModeSelector.ModeUpdate -= OnModeUpdate;
        _playButton.onClick.RemoveListener(() => GameStartButtonClicked?.Invoke());
    }
}
