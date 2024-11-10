using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GridCell gridCellPrefab;
    [SerializeField] private GameObject linePrefab;
    private GameAssetsLoader gameAssetsLoader;

    [Space(5)]
    [SerializeField] private List<SymbolSkin> symbolSkinsList;


    private GridCell[,] board;
    private BoardEvaluator boardEvaluator;
    private InputService inputService;

    public event Action<GridCell> CellClicked;

    public void Initialize(InputService inputService, GameAssetsLoader gameAssetsLoader)
    {
        boardEvaluator = new BoardEvaluator();
        this.inputService = inputService;
        this.gameAssetsLoader = gameAssetsLoader;
    }

    public GridCell[] GetEmptyCells()
    {
        return board.Cast<GridCell>()
                     .Where(cell => cell.IsCellEmpty)
                     .ToArray();
    }

    public bool HasEmptyCells()
    {
        var emptyCells = GetEmptyCells();
        return emptyCells.Length > 0;
    }

    public void ClearPlayingField()
    {
        board.Cast<GridCell>().ToList().ForEach(cell => cell.ResetCell());
    }

    public PlayerSymbolType? GetWinnerSymbol()
    {
        return boardEvaluator.GetWinnerSymbol(board);
    }

    public GridCell[] GetWinningCombination()
    {
        return boardEvaluator.GetWinningCombination(board);
    }

    private void OnCellClicked(GridCell gridCell)
    {
        CellClicked?.Invoke(gridCell);
    }

    public async UniTask SelectCell(PlayerSymbolType playerSymbolType, GridCell selectedCell)
    {
        var symbolSprite = GetSpriteForSymbol(playerSymbolType);
        await selectedCell.SelectCell(playerSymbolType, symbolSprite);
    }

    private Sprite GetSpriteForSymbol(PlayerSymbolType symbolType)
    {
        var symbolSkin = (symbolType == PlayerSymbolType.Cross)
            ? gameAssetsLoader.XSymbolSprite : gameAssetsLoader.OSymbolSprite;

        if (symbolSkin != null)
        {
            return symbolSkin;
        }

        Debug.LogWarning($"Sprite not found for symbol type: {symbolType}");
        return null;
    }

    public async UniTask DrawGameBoard()
    {
        await DrawGridCell();
        DrawBoardLinesAsync();
    }

    private async UniTask DrawGridCell()
    {
        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gridSize;

        board = new GridCell[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                var cell = Instantiate(gridCellPrefab, gridLayoutGroup.transform);
                cell.Initialization(inputService);
                cell.CellClicked += OnCellClicked;
                board[x, y] = cell;
            }
        }

        await UniTask.Yield();
    }

    private void DrawBoardLinesAsync()
    {
        Vector2 cellSize = gridLayoutGroup.cellSize;
        Vector2 spacing = gridLayoutGroup.spacing;

        float lineThickness = spacing.x / 2;

        int columns = gridLayoutGroup.constraintCount;
        int rows = Mathf.CeilToInt((float)gridLayoutGroup.transform.childCount / columns);

        float startX = -(columns * cellSize.x + (columns - 1) * spacing.x) / 2;
        float startY = (rows * cellSize.y + (rows - 1) * spacing.y) / 2;

        for (int i = 1; i < columns; i++)
        {
            GameObject verticalLine = Instantiate(linePrefab, transform);
            verticalLine.name = $"GridLine_Vertical_{i}";

            RectTransform lineRectTransform = verticalLine.GetComponent<RectTransform>();
            lineRectTransform.sizeDelta = new Vector2(lineThickness, 0);
            float xPos = startX + i * cellSize.x + (i - 0.5f) * spacing.x;
            lineRectTransform.anchoredPosition = new Vector2(xPos, 0);

            float targetHeight = (rows * cellSize.y + (rows - 1) * spacing.y) * 1.2f;
            lineRectTransform.sizeDelta = new Vector2(lineThickness, targetHeight);
        }

        for (int i = 1; i < rows; i++)
        {
            GameObject horizontalLine = Instantiate(linePrefab, transform);
            horizontalLine.name = $"GridLine_Horizontal_{i}";

            RectTransform lineRectTransform = horizontalLine.GetComponent<RectTransform>();
            lineRectTransform.sizeDelta = new Vector2(0, lineThickness);
            float yPos = startY - i * cellSize.y - (i - 0.5f) * spacing.y;
            lineRectTransform.anchoredPosition = new Vector2(0, yPos);

            float targetWidth = (columns * cellSize.x + (columns - 1) * spacing.x) * 1.2f;
            lineRectTransform.sizeDelta = new Vector2(targetWidth, lineThickness);
        }
    }

}

[Serializable]
public class SymbolSkin
{
    public PlayerSymbolType SymbolType { get; }
    public Sprite Skin { get; }

    public SymbolSkin(PlayerSymbolType playerSymbolType , Sprite skin)
    {
        Skin = skin;
        SymbolType = playerSymbolType;
    }
}