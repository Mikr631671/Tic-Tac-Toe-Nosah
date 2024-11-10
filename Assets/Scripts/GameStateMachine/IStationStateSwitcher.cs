using Cysharp.Threading.Tasks;


public interface IStationStateSwitcher
{
    UniTaskVoid SwitchState<T>() where T : IState;
    public IState CurrentState { get; }
}