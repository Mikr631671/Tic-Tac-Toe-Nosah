using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Image _cellIcon;
    [SerializeField] private RectTransform _targetImageRectTransform;

    [SerializeField] private float _animationDuration = 0.15f;

    private PlayerSymbolType? _playerSymbolType = null;
    private InputService _inputService;

    public event Action<GridCell> CellClicked;

    public bool IsCellEmpty => _playerSymbolType == null;

    public PlayerSymbolType? PlayerSymbolType => _playerSymbolType;

    public async UniTask SelectCell(PlayerSymbolType playerSymbolType, Sprite sprite)
    {
        _cellIcon.sprite = sprite;
        _playerSymbolType = playerSymbolType;

        await _cellIcon.transform.DOScale(Vector3.one, _animationDuration)
                                 .SetEase(Ease.OutBack)
                                 .ToUniTask();
    }

    public void ResetCell()
    {
        _playerSymbolType = null;
        _cellIcon.sprite = null;
        _cellIcon.transform.localScale = Vector3.zero;
    }

    public void Initialization(InputService inputService)
    {
        _inputService = inputService;
        _inputService.Click += OnClick;
    }

    private void OnClick(Vector2 clickPosition)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_targetImageRectTransform, clickPosition, null, out Vector2 localPoint))
        {
            if (_targetImageRectTransform.rect.Contains(localPoint))
            {
                if (IsCellEmpty)
                {
                    CellClicked?.Invoke(this);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (_inputService != null)
        {
            _inputService.Click -= OnClick;
        }
    }
}
