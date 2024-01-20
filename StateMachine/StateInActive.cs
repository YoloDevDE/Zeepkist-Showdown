using Showdown3.Commands;
using Showdown3.StateMachine.Contexts;
using Showdown3.StateMachine.States;
using ZeepSDK.Messaging;

namespace Showdown3.StateMachine;

public class StateInActive : IState
{
    public IContext Context { get; }

    public StateInActive(IContext context)
    {
        Context = context;
    }

    public void Enter()
    {
        CommandStart.OnHandle += OnStart;
        CommandStop.OnHandle += OnStop;
    }

    private void OnStop()
    {
        MessengerApi.LogError("Application is already stopped!");
    }


    public void Exit()
    {
        CommandStart.OnHandle -= OnStart;
        CommandStop.OnHandle -= OnStop;
        
    }

    private void OnStart()
    {
        Context.SwitchState(new StateActive(Context));
    }
}