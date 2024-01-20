using Showdown3.Commands;
using Showdown3.StateMachine.Contexts;
using UnityEngine.Networking.Types;
using ZeepkistClient;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.States;

public class StateCheckIn : IState
{
    public StateCheckIn(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

    public void Enter()
    {
        CommandContinue.OnHandle += OnContinue;
        
        ChatApi.SendMessage("/settime 86400");
        ChatApi.SendMessage("/servermessage orange 0 Waiting for Participants...");
        
    }
    public void Exit()
    {
        CommandContinue.OnHandle -= OnContinue;
    }
    private void OnContinue()
    {
        Context.SwitchState(new StateDraft(Context));
    }


}