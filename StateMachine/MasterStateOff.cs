using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Messaging;

namespace Showdown3.StateMachine;

public class MasterStateOff : IState
{
    public MasterStateOff(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }


    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        CommandStart.OnHandle += OnCommandStart;
        CommandStop.OnHandle += OnCommandStop;
    }


    public void Exit()
    {
        CommandStart.OnHandle -= OnCommandStart;
        CommandStop.OnHandle -= OnCommandStop;
    }

    private void OnCommandStop()
    {
        TaggedMessenger.Value.LogWarning("already stopped");
    }

    private void OnCommandStart()
    {
        StateMachine.TransitionTo(new MasterStateOn(StateMachine));
    }
}