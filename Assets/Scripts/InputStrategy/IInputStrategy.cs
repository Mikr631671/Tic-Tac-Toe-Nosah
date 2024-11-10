using Cysharp.Threading.Tasks;

public interface IInputStrategy
{
    public void Initialize(GameBoard gameBoard);

    public UniTask<GridCell> GetSelectedCellAsync();
}