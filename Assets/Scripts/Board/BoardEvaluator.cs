public class BoardEvaluator
{
    public GridCell[] GetWinningCombination(GridCell[,] board)
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            if (CheckLine(board[row, 0], board[row, 1], board[row, 2]))
                return new[] { board[row, 0], board[row, 1], board[row, 2] };
        }

        for (int col = 0; col < board.GetLength(1); col++)
        {
            if (CheckLine(board[0, col], board[1, col], board[2, col]))
                return new[] { board[0, col], board[1, col], board[2, col] };
        }

        if (CheckLine(board[0, 0], board[1, 1], board[2, 2]))
            return new[] { board[0, 0], board[1, 1], board[2, 2] };

        if (CheckLine(board[0, 2], board[1, 1], board[2, 0]))
            return new[] { board[0, 2], board[1, 1], board[2, 0] };

        return new GridCell[0];
    }

    public PlayerSymbolType? GetWinnerSymbol(GridCell[,] board)
    {
        var winningCombination = GetWinningCombination(board);
        return winningCombination.Length > 0 ? winningCombination[0].PlayerSymbolType : null;
    }

    private bool CheckLine(GridCell cell1, GridCell cell2, GridCell cell3)
    {
        return cell1.PlayerSymbolType.HasValue &&
               cell1.PlayerSymbolType == cell2.PlayerSymbolType &&
               cell2.PlayerSymbolType == cell3.PlayerSymbolType;
    }
}
