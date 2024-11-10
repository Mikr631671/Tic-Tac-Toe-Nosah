using System;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float _elapsedTime;
    private bool _isRunning;

    public TimeSpan ElapsedTime => TimeSpan.FromSeconds(_elapsedTime); 

    private void Update()
    {
        if (_isRunning)
        {
            _elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartTimer()
    {
        _elapsedTime = 0f;
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        var time = TimeSpan.FromSeconds(_elapsedTime);
        timerText.text = $"{time.Minutes:D2}:{time.Seconds:D2}";
    }
}
