using Cysharp.Threading.Tasks;

public interface IPlayer
{
    public void Initialize(GameBoard gameBoard);

    public UniTask<GridCell> GetMove();
}