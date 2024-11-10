using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Managers;


public class StateMachine : IStationStateSwitcher
{
    private readonly List<IState> _allStates = new();

    public IState CurrentState { get; private set; }

    public async UniTask Initialize()
    {
        CurrentState = _allStates[0];
        await CurrentState.Enter();
    }

    public StateMachine AddState(IState state)
    {
        _allStates.Add(state);
        return this;
    }

    public async UniTaskVoid SwitchState<T>() where T : IState
    {
        await CurrentState.Exit();
        var state = _allStates.FirstOrDefault(s => s is T);
        CurrentState = state;

        if (CurrentState != null)
            await CurrentState.Enter();
    }
}
