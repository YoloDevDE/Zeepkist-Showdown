using Showdown3.StateMachine.States;

namespace Showdown3.StateMachine.Contexts;

public interface IContext
{
    public IState State { get; set; }

    public void SwitchState(IState state);

}