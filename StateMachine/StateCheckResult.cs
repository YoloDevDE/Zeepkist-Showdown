using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine;

public class StateCheckResult : IState
{
    public StateCheckResult(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        //test  
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        // if (Context.Match.CheckIfWinContitionIsMet())
        //     StateMachine.TransitionTo(new StateWaitForCommand(StateMachine));
        // else
        //     StateMachine.TransitionTo(new StateReadyCheck(StateMachine));
    }

    public void Exit()
    {
    }
}