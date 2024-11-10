using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModeSelector : MonoBehaviour
{
    [SerializeField] private List<GameModeButton> _gameModeButtons;

    public event Action<GameMode> ModeUpdate;

    public void Initialize()
    {
        _gameModeButtons.ForEach(button => button.Initialize());
        _gameModeButtons.ForEach(button => button.ModeSelect += OnModeSelect);

        var firstMode = _gameModeButtons.FirstOrDefault();
        firstMode?.SelectMode();
    }

    private void OnModeSelect(GameModeButton gameModeButton)
    {
        _gameModeButtons.ForEach(button => {
            var isSelected = button == gameModeButton;
            button.SetModeButtonSelected(isSelected);
            });

        ModeUpdate?.Invoke(gameModeButton.GameMode);
    }

    private void OnDestroy()
    {
        _gameModeButtons.ForEach(button => button.ModeSelect -= OnModeSelect);
    }
}
