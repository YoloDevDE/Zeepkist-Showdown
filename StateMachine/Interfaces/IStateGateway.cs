namespace Showdown3.StateMachine.Interfaces;

public interface IStateGateway : IState
{
    public IContext SubContext { get; }
}