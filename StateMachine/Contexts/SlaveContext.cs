using Showdown3.StateMachine.States;

namespace Showdown3.StateMachine.Contexts;

public class SlaveContext : IContext
{
    public IState State { get; set; }

    public SlaveContext()
    {
        State = new StateCheckIn(this);
        State.Enter();
    }
    public void SwitchState(IState state)
    {
        State.Exit();
        State = state;
        State.Enter();
    }

    public void SynchroniseStateWithServer()
    {
        SwitchState(new StateCheckIn(this));
    }
}