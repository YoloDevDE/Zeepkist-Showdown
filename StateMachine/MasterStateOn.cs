using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Messaging;

namespace Showdown3.StateMachine;

public class MasterStateOn : IMasterState
{
    public MasterStateOn(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        CurrentState = new StateShowdownMain(this);
    }

    public IStateMachine StateMachine { get; }
    public IState CurrentState { get; set; }

    public void Enter()
    {
        CommandStart.OnHandle += OnCommandStart;
        CommandStop.OnHandle += OnCommandStop;
        TaggedMessenger.Value.LogSuccess("started");
        CurrentState.Enter();
    }


    public void Exit()
    {
        CommandStart.OnHandle -= OnCommandStart;
        CommandStop.OnHandle -= OnCommandStop;
        TaggedMessenger.Value.LogSuccess("stopped");
        CurrentState.Exit();
    }

    private void OnCommandStop()
    {
        StateMachine.TransitionTo(new MasterStateOff(StateMachine));
    }

    private void OnCommandStart()
    {
        TaggedMessenger.Value.LogWarning("already started");
    }
}