using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Messaging;

namespace Showdown3.StateMachine;

public class MasterStateMachine : IStateMachine
{

    public MasterStateMachine()
    {
        CurrentState = new MasterStateOff(this);
        CurrentState.Enter();
    }
    public void Test(){}
    public IState CurrentState { get; set; }
}